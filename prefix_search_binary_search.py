#!/bin/env python

""" Test search prefix using binary search """

import os
import bisect
import curses
from pprint import pprint as p

def binary_search(sequence, element):
	""" original and classic binary search """
	min = 0
	max = len(sequence) - 1
	while True:
		if max < min:
			return -1
		m = (min + max) // 2
		if sequence[m] == element:
			return m
		elif sequence[m] < element:
			min = m + 1
		elif sequence[m] > element:
			max = m - 1

def prefix_search(element, sequence, recursive=True):
	"""
	Prefix search using binary search algorithm
	recursive=False: if element not found as a prefix of any word in sequence simply return -1
	recursive=True:  if element not found as a prefix of any word in sequence check if every
	prefix of that element match any prefix in sequence, if not again return -1
	"""
	min = 0
	max = len(sequence) - 1
	prefix_index = -1
	while True:
		if max < min:
			if prefix_index != -1:
				return prefix_index
			else: # see if every prefix of your own string match any prefix in the list
				if recursive is True:
					for i in range(len(element) + 1):
						temp_index = prefix_search(element[:i], sequence, recursive=False)
						if temp_index != -1:
							prefix_index = temp_index
						else:
							return prefix_index
					return -1
				else:
					return -1
		m = (min + max) // 2
		if sequence[m].startswith(element):
			prefix_index = m
		if sequence[m] < element:
			min = m + 1
		elif sequence[m] > element:
			max = m - 1
		elif sequence[m] == element:
			return m

def get_word_list():
	""" Get word list from file """
	cwd = os.getcwd()
	path = os.path.join(cwd, 'english_words.txt')
	with open(path, 'r') as file:
		words = [word.strip() for word in file.readlines()]
		return words

class SpellCheck(object):
	""" Spellcheck word based on self.vocabulary """
	def __init__(self, source):
		self.vocabulary = source

	def candidates(self, word):
		""" Generate possible spelling corrections for word. """
		return (self._known([word])
				or self._known(self._edits1(word))
				or self._known(self._edits2(word))
				or [word])

	def _known(self, words):
		""" The subset of `words` that appear in the dictionary of WORDS. """
		return set(w for w in words if w in self.vocabulary)

	def _edits1(self, word):
		""" All edits that are one edit away from `word`. """
		letters   = 'abcdefghijklmnopqrstuvwxyz'
		splits    = [(word[:i], word[i:])    for i in range(len(word) + 1)]
		deletes   = [L + R[1:]               for L, R in splits if R]
		swap      = [L + R[1] + R[0] + R[2:] for L, R in splits if len(R) > 1]
		replaces  = [L + c + R[1:]           for L, R in splits if R for c in letters]
		inserts   = [L + c + R               for L, R in splits for c in letters]
		return set(deletes + swap + replaces + inserts)

	def _edits2(self, word):
		""" All edits that are two edits away from `word`. """
		return (e2 for e1 in self._edits1(word) for e2 in self._edits1(e1))

def get_autocomplete_list(word, words, max=20):
	"""
	Get autocomplete list for word by searching for the longest prefix in words contain word
	"""
	idx = prefix_search(word, words)
	autocomplete = []
	for i in range(max):
		if idx + i < len(words):
			autocomplete.append(words[idx + i])
		else:
			return autocomplete
	return autocomplete

def main(win):
	english_words = get_word_list()
	spellcheck = SpellCheck(english_words)

	word = ''
	search_index = -1
	win.clear()
	win.addstr('Search: ')
	while True:
		try:
			key = win.getkey()
			if key in ('KEY_BACKSPACE', '\b', '\x7f'):
				word = word[:-1] # delete last character
			elif key in (' '):
				# brwk
				candidates = spellcheck.candidates(word)

				win.clear()
				win.addstr('Search: ' + word + "\n")
				for candidate in candidates:
					win.addstr(candidate + "\n")

				win.addstr('Press enter to continue')
				key = win.getch()
				continue
			else:
				word += str(key)

			win.clear()
			win.addstr('Search: ' + word + "\n")

			autocomplete = get_autocomplete_list(word, english_words)
			for w in autocomplete:
				win.addstr(w + "\n")

			if key == os.linesep:
				win.addstr('You typed: ' + word)
				key = win.getch()
				break
		except Exception as e:
			# No input
			pass

if __name__ == '__main__':
	curses.wrapper(main)

# vim: nofoldenable
