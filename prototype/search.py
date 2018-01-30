#!/bin/env python

""" prefix search for dictionary module """

from bisect import bisect_left

from util import get_word_list

def _binary_search(element, sequence):
	""" a simplified demonstration of binary search algorithm - dont use this """
	min_pos = 0
	max_pos = len(sequence) - 1
	while True:
		if max_pos < min_pos:
			return -1
		cur_pos = (min_pos + max_pos) // 2
		if sequence[cur_pos] == element:
			return cur_pos
		elif sequence[cur_pos] < element:
			min_pos = cur_pos + 1
		elif sequence[cur_pos] > element:
			max_pos = cur_pos - 1

def binary_search(element, sequence, low=0, high=None):  # can't set default for hi here
	""" wrapper around bisect_left(), use standard library to reduce bug chance """
	if high is None:
		high = len(sequence)
	pos = bisect_left(sequence, element, low, high)  # find insertion position
	if pos != high and sequence[pos] == element: # don't walk off the end
		return pos
	return -1

def prefix_search(element, sequence, recursive=True):
	"""
	Prefix search using binary search algorithm
	recursive=False: if element not found as a prefix of any word in sequence simply return -1
	recursive=True:  if element not found as a prefix of any word in sequence check if every
	prefix of that element match any prefix in sequence, if not again return -1
	"""
	min_pos = 0
	max_pos = len(sequence) - 1
	prefix_index = -1
	while True:
		if max_pos < min_pos:
			if prefix_index != -1:
				return prefix_index
			if recursive is True:
				for i in range(len(element) + 1):
					temp_index = prefix_search(element[:i], sequence, recursive=False)
					if temp_index != -1:
						prefix_index = temp_index
					else:
						return prefix_index
				return -1
			return -1
		cur_pos = (min_pos + max_pos) // 2
		if sequence[cur_pos].startswith(element):
			prefix_index = cur_pos
		if sequence[cur_pos] < element:
			min_pos = cur_pos + 1
		elif sequence[cur_pos] > element:
			max_pos = cur_pos - 1
		elif sequence[cur_pos] == element:
			return cur_pos

def main():
	english_words = get_word_list()

	try:
		while True:
			query = input('Enter a prefix or first part of a common word: ')
			pos = prefix_search(query, english_words)
			words = [english_words[pos + i] for i in range(20)]
			for word in words:
				print(word)
	except KeyboardInterrupt:
		pass

if __name__ == '__main__':
	main()

# vim: nofoldenable
