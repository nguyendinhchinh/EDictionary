#!/bin/env python

""" utility functions that other modules may use """

import os

def get_word_list():
	""" get word list from file """
	dirname = os.path.dirname(os.path.realpath(__file__))
	path = os.path.join(dirname, 'english_words.txt')
	with open(path, 'r') as file:
		words = [word.strip() for word in file.readlines()]
		return words

def main():
	""" main function """
	print(get_word_list())

if __name__ == '__main__':
	main()

# vim: nofoldenable
