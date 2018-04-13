#!/bin/env python3

""" scrap wordlist from oxford site """

import json
import logging
import os
import sys
import time
import traceback
import urllib.request

import colorama
from exceptions import ConnectionError, HTTPError, Timeout
from api.oxford import Word, WordNotFound
from util import quote, mkdir, touch, read, put, settup_logger, timer

# disable requests logging (enable by default when importing)
logging.getLogger("requests").setLevel(logging.CRITICAL)

# init color is required on windows
if sys.platform != 'linux': # windows or other
	colorama.init()


# pylint: disable=invalid-name

# -- module-scoped variables --

# setup color
GREEN = colorama.Fore.GREEN
MAGENTA = colorama.Fore.MAGENTA
BLUE = colorama.Fore.BLUE
YELLOW = colorama.Fore.YELLOW
RESET = colorama.Fore.RESET

NOT_FOUND_WORDS_FILE = os.path.join('cache', 'not_found_words.txt')
SKIPPED_WORDS_FILE = os.path.join('cache', 'skipped_words.txt')
CORRUPTED_WORDS_FILE = os.path.join('cache', 'corrupted_words.txt')

WORDLIST_FILE = 'words_alpha.txt'

AUDIO_PATH = os.path.join(os.getcwd(), 'data', 'audio')
DEF_PATH = os.path.join(os.getcwd(), 'data', 'words')

NEED_AUDIO = False

LOG_PATH = os.path.join(os.getcwd(), 'cache', 'scraping.log')
LOG = settup_logger('info', LOG_PATH, level=logging.INFO)

def get_not_found_words():
	""" return a dictionary of not found words (in oxford diciontary) """
	path = os.path.join(os.getcwd(), NOT_FOUND_WORDS_FILE)
	return read(path, isdict=True)

def get_downloaded_words():
	""" get a list of json files to determine which words
	have been downloaded before (to skip it next time)
	'abc.json' -> 'abc'
	'abc_1.json' -> 'abc'
	'ABC.D.json' -> 'abc.d'
	"""
	return {os.path.splitext(file)[0].split('_')[0].lower(): None
			for file in os.listdir(DEF_PATH) if os.path.isfile(os.path.join(DEF_PATH, file))}

DOWNLOADED_WORDS = get_downloaded_words()
NOT_FOUND_WORDS = get_not_found_words()


def download(url, directory):
	""" download url to directory
	argument: (r'https://abc/innocent_file.exe', r'C:\Program Files\system32\')
	> download file to C:\Program Files\system32\innocent_file.exe
	"""
	path = os.path.join(directory, url.split('/')[-1])
	urllib.request.urlretrieve(url, path)

def save(word, path):
	""" write data to path in json format with filename is word id """
	if word is not None:
		filename = word['id']
		cache_path = os.path.join(path, filename + '.json')
		touch(cache_path)

		with open(cache_path, 'w') as file:
			json.dump(word, file, separators=(',', ':')) # minify

def update_skipped_words(word):
	""" update words that has to be skipped (connection error) """
	path = os.path.join(os.getcwd(), SKIPPED_WORDS_FILE)
	put(word, path)

def update_not_found_words(word):
	""" update not found words (in oxford diciontary) """
	path = os.path.join(os.getcwd(), NOT_FOUND_WORDS_FILE)
	put(word, path)

def update_corrupted_words(word):
	""" update words that has corrupted data for some reasons """
	path = os.path.join(os.getcwd(), CORRUPTED_WORDS_FILE)
	put(word, path)

def get_wordlist(filename):
	""" read file in current working directory for wordlist """
	path = os.path.join(os.getcwd(), filename)
	return read(path)

def download_audios(word):
	""" download all audio files from word """
	for pronunciation in word['pronunciations']:
		url = pronunciation['url']

		try:
			if url is not None:
				download(url, AUDIO_PATH)
		except urllib.error.HTTPError: # 'ginkgo': audio urls are available but not valid
			update_corrupted_words(word + ':audio')

def remove_urls(word):
	""" replace word urls with audio filenames """
	for i, _ in enumerate(word['pronunciations']):
		url = word['pronunciations'][i]['url']

		try:
			filename = url.rsplit('/', 1)[1]
			word['pronunciations'][i]['filename'] = filename
		except AttributeError: # NoneType has no attribute 'rsplit'
			word['pronunciations'][i]['filename'] = None
		finally:
			word['pronunciations'][i].pop('url', None)

	return word

@timer
def extract_data(word):
	""" get word info using api and save data in filesystem

	argument: word name to extract data from
	return (statuscode, references)

	status code:
		0: success
		1: word not found (error 404)
		2: connection error
		3: corrupted data
	references: a list of other ids (same word with different wordforms)
	"""
	try:
		print("Request html page of '{}'...".format(word))
		LOG.info("Request html page of '%s'...", word)

		Word.get(word)
	except WordNotFound: # 404
		print("No data for '{}' word. Skipping".format(word))
		LOG.info("No data for '%s' word. Skipping", word)

		update_not_found_words(word)
		return (1, None)
	except (ConnectionError, HTTPError, Timeout) as error:
		print("Requests failed: '{}'".format(error))
		LOG.debug("Requests failed: '%s'", error)

		update_skipped_words(word)
		return (2, None)

	try:
		print("Extracting data from '{}'...".format(word))
		LOG.info("Extracting data from '%s'...", word)

		data = Word.info()

		if NEED_AUDIO:
			# download pronounce audio file
			print("Downloading audio file of '{}'...".format(word))
			LOG.info("Downloading audio file of '%s'...", word)

			download_audios(data)
		data = remove_urls(data)

		# save word information (definitions, examples, idioms,...)
		print("saving '{}' in json format to {}...".format(word, DEF_PATH))
		LOG.info("saving '%s' in json format to %s...", word, DEF_PATH)

		save(data, DEF_PATH)

	except Exception as error:
		print(traceback.format_exc())
		LOG.debug(traceback.format_exc())

		update_corrupted_words(word)
		return (3, None)
	else:
		return (0, data['similar'])

def scrap(words, *, reference=True, force=False):
	""" scrap list of words
	argument: ([word1, word2, ...], reference=True, force=False)
	reference (bool): scrap other wordforms of a word
	force (bool): force redownload word info. Used in debugging
	"""
	for word in words:
		print('scraping ' + GREEN + quote(word) + RESET + '...')

		if word in DOWNLOADED_WORDS and force is False:
			print(GREEN + quote(word) + RESET + 'has been ' + YELLOW + 'downloaded' + RESET + '. Skipping to next word')
			continue
		elif word in NOT_FOUND_WORDS and force is False:
			print(GREEN + quote(word) + YELLOW + ' not found' + RESET + '. Skipping to next word')
			continue
		else: # valid word. Downloading...
			print('valid word: ' + GREEN + quote(word) + RESET + '. Ready to ' + BLUE + 'download' + RESET)
			exitcode, others = extract_data(word)

		if exitcode == 1: # Word not found
			time.sleep(0.5)
		elif exitcode == 2: # Connection error
			time.sleep(10)
		elif exitcode == 3: # Word data is corrupted
			time.sleep(2)
		else: # success
			if others and reference is True:
				print('scrap ' + BLUE + 'reference' +  RESET + ' words: ' + GREEN + quote(others) + RESET)
				scrap(others, reference=False)

			print(MAGENTA + 'cooldown...' + RESET) # cooldown time: 2s
			if extract_data.elapsed < 2:
				time.sleep(2 - extract_data.elapsed)

def run(filename=WORDLIST_FILE, *, force=False):
	""" read the wordlist from a file and scrap it """

	print('getting wordlist data from {}'.format(os.path.join(os.getcwd(), filename)))
	words = get_wordlist(filename)

	scrap(words, reference=True, force=force)

def run_corrupted_words(force=False):
	""" scrape from corrupted word list """
	run(CORRUPTED_WORDS_FILE, force=force)

def run_skipped_words(force=False):
	""" scrape from skipped word list """
	run(SKIPPED_WORDS_FILE, force=force)

def test(s=0):
	""" testing stuff """
	if s == 0: # valid words
		scrap(['hello', 'world'], force=True)
	elif s == 1: # not found words
		scrap(['neineineinei', 'kekekeke'], force=True)

# vim: nofoldenable
