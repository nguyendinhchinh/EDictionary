#!/bin/env python

""" Test search prefix using binary search """

import os
import bisect
import curses
from pprint import pprint as p

def get_word_list(path):
	""" Get word list from file """
	with open(path, 'r') as file:
		words = [word.strip() for word in file.readlines()]
		return words

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

def main(win):
	# setup english dictionary
	cwd = os.getcwd()
	english_words_path = os.path.join(cwd, 'english_words.txt')
	english_words = get_word_list(english_words_path)

	word = ''
	search_index = -1
	win.clear()
	win.addstr('Search: ')
	while True:
		try:
			key = win.getkey()
			if key in ('KEY_BACKSPACE', '\b', '\x7f'):
				word = word[:-1] # delete last character
			else:
				word += str(key)

			search_index = prefix_search(word, english_words)
			win.clear()
			win.addstr('Search: ' + word + "\n")
			for i in range(20):
				win.addstr(english_words[search_index + i] + "\n")
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
