#!/bin/env python

""" Test search prefix using binary search """

import os
import curses

from spellcheck import SpellCheck
from search import prefix_search, binary_search
from util import get_word_list

class Definition(object):
	""" data structure for definition of a word in dictionary """
	def __init__(self, word):
		self.word = word
		self.verb_meaning = []
		self.verb_examples = []
		self.noun_meaning = []
		self.noun_examples = []
		self.adj_meaning = []
		self.adj_examples = []
		self.adv_meaning = []
		self.adv_examples = []

	def __str__(self):
		print('definition of {}:'.format(self.word))
		print('verb: ')
		for verb, verb_example in zip(self.verb_meaning, self.verb_examples):
			print(' ' + verb + "\n" + verb_example)
		print('noun: ')
		for noun, noun_example in zip(self.noun_meaning, self.noun_examples):
			print(' ' + noun + "\n" + noun_example)
		print('adj: ')
		for adj, adj_example in zip(self.adj_meaning, self.adj_examples):
			print(' ' + adj + "\n" + adj_example)
		print('adv: ')
		for adv, adv_example in zip(self.adv_meaning, self.adv_examples):
			print(' ' + adv + "\n" + adv_example)

class Word(object):
	""" Word is used to store word value (string) and its definition (Definition) """
	def __init__(self, name, definition=None):
		self.name = name
		self.definition = definition

	def __str__(self):
		print(self.name)

	def get_definition(self):
		""" return definition of word if it's in the database """
		return '<Definition of {}>'.format(self.name)

	def print_def(self):
		print(str(self.definition))

class DictionaryCore(object):
	""" dictionary implementation. database is a list of words (Word) """
	def __init__(self, vocabulary):
		self.data = vocabulary
		self.completion = []
		self.completion_start_idx = 0
		self.spellcheck = SpellCheck(self.get_wordlist())

	def get_completion_list(self, prefix, max=20):
		"""
		get list of n `max` number of suggestions for `prefix` by
		searching for the longest prefix in self.data that contain `prefix`
		"""
		self.completion_start_idx = prefix_search(prefix, self.data)
		autocomplete = []
		for i in range(max):
			if self.completion_start_idx + i < len(self.data):
				autocomplete.append(self.data[self.completion_start_idx + i].name)
			else:
				return autocomplete
		return autocomplete

	def get_related_words(self, word):
		""" return a list of word similar to the word just entered but not found """
		return self.spellcheck.candidates(word)

	def get_wordlist(self):
		""" return a list of words (string) in the dictionary """
		return [word.name for word in self.data]

	def add_word(self, word):
		pass

	def remove_word(self, word):
		pass

class DictionaryUI(DictionaryCore):
	""" simple dictionary demo that use interface from curses module """
	KEY_ESC = chr(27)
	KEY_ENTER = "\n"
	KEY_BACKSPACE = curses.KEY_BACKSPACE
	KEY_RESIZE = curses.KEY_RESIZE
	KEY_UP = curses.KEY_UP
	KEY_DOWN = curses.KEY_DOWN
	PRINTABLE_KEYS = [chr(i) for i in range(32, 127)]

	CYAN = curses.COLOR_CYAN
	RED = curses.COLOR_RED
	YELLOW = curses.COLOR_YELLOW
	BLUE = curses.COLOR_BLUE
	MAGENTA = curses.COLOR_MAGENTA
	BLACK = curses.COLOR_BLACK
	WHITE = 15

	def __init__(self, stdscr, vocabulary, wordlist_width=25):
		super().__init__(vocabulary)
		curses.use_default_colors() # support transparancy in application
		self.stdscr = stdscr # main window
		self.input_buffer = ''

		# Start colors in curses
		curses.start_color()
		#                        fg    bg
		curses.init_pair(1, self.CYAN, -1)
		curses.init_pair(2, self.BLUE, -1)
		curses.init_pair(3, self.YELLOW, -1)
		curses.init_pair(4, self.BLACK, self.WHITE)

		# create derived windows
		input_hwyx = (3, wordlist_width, 0, 0)
		wordlist_hwyx = (curses.LINES-input_hwyx[0], wordlist_width, input_hwyx[0], 0)
		definition_hwyx = (curses.LINES, curses.COLS - wordlist_hwyx[1], 0, wordlist_hwyx[1])

		self.win_input = stdscr.derwin(*input_hwyx)
		self.win_wordlist = stdscr.derwin(*wordlist_hwyx)
		self.win_definition = stdscr.derwin(*definition_hwyx)

		self.win_input.keypad(True) # special key will be interpreted by curses

		# set foreground to cyan
		self.win_input.attrset(curses.color_pair(1))
		self.win_wordlist.attrset(curses.color_pair(1))
		self.win_definition.attrset(curses.color_pair(1))

		# # windows size variables
		# h, w = self.stdscr.getmaxyx()
		# h_i, w_i = self.win_input.getmaxyx()
		# h_w, w_w = self.win_wordlist.getmaxyx()
		# h_d, w_d = self.win_definition.getmaxyx()

		# # # input marker
		# self.win_input.addch(0, 0, '*', curses.A_REVERSE)
		# self.win_input.addch(h_i-1, 0, '*', curses.A_REVERSE)
		# self.win_input.addch(0, w_i-1, '*', curses.A_REVERSE)
		# self.win_input.insch(h_i-1, w_i-1, '*', curses.A_REVERSE)
		# # # wordlist marker
		# self.win_wordlist.addch(0, 0, '*', curses.A_REVERSE)
		# self.win_wordlist.addch(h_w-1, 0, '*', curses.A_REVERSE)
		# self.win_wordlist.addch(0, w_w-1, '*', curses.A_REVERSE)
		# self.win_wordlist.insch(h_w-1, w_w-1, '*', curses.A_REVERSE)
		# # # definition marker
		# self.win_definition.addch(0, 0, '*', curses.A_REVERSE)
		# self.win_definition.addch(h_d-1, 0, '*', curses.A_REVERSE)
		# self.win_definition.addch(0, w_d-1, '*', curses.A_REVERSE)
		# self.win_definition.insch(h_d-1, w_d-1, '*', curses.A_REVERSE)

	def run(self):
		""" run endless loop waiting for user input """
		self.redraw_ui()
		self.draw_startup_screen()
		try:
			while True:
				key = self.win_input.get_wch()
				if key == self.KEY_ESC: # quit
					break
				elif key == self.KEY_ENTER: # enter query
					self.display_definition()
				elif key in self.PRINTABLE_KEYS:
					self.input_buffer += key
					self.redraw_wordlist_on_completion()
				elif key == self.KEY_BACKSPACE: # delete character
					self.input_buffer = self.input_buffer[:-1]
					self.redraw_wordlist_on_completion()
				elif key == self.KEY_RESIZE: # terminal resize event
					self.resize()
				elif key == self.KEY_DOWN: # navigate up the wordlist
					self.redraw_wordlist_on_scrolling(key)
				elif key == self.KEY_UP: # navigate down the wordlist
					self.redraw_wordlist_on_scrolling(key)
				else:
					continue
				self._print_key(key) # debug
				self.redraw_input()
		except KeyboardInterrupt:
			pass

	def draw_startup_screen(self):
		""" print startup screen """
		height, width = self.win_definition.getmaxyx()
		startup_str = [
				"This is a dictionary prototype",
				"Type something...",
				"Or press <Esc> to exit"
				]

		#  +--------------------------------+-------------------------------+
		#  | odd - odd (width - str length) |          odd - even           |
		#  |--------------------------------+-------------------------------|
		#  | 1 2 3 4 5 6 7 8 9 10 11 12 13  | 1 2 3 4 5 6 7 8 9 10 11 12 13 |
		#  |         1 2 3 4 5              |           1 2 3 4             |
		#  | 13 // 2 = 6                    | 13 // 2 = 6                   |
		#  | 5 // 2 = 2                     | 4 // 2 = 2                    |
		#  | 6 - (2 - 1) = 5                | 7 - (2 - 1) = 6               |
		#  |--------------------------------+-------------------------------|
		#  |          even - odd            |         even - even           |
		#  |--------------------------------+-------------------------------|
		#  | 1 2 3 4 5 6 7 8 9 10 11 12     |  1 2 3 4 5 6 7 8 9 10 11 12   |
		#  |         1 2 3 4 5              |          1 2 3 4              |
		#  | 12 // 2 = 6                    |  12 // 2 = 6                  |
		#  | 5 // 2 = 2                     |  4 // 2 = 2                   |
		#  | 6 - (2 - 1) = 5                |  6 - (2 - 1) = 5              |
		#  +--------------------------------+-------------------------------+
		# -> formula: STR_START_X = (WIDTH // 2) - ((STR_LENGTH // 2) - 1)

		start_y = (height // 2) - 1 # printing 3 lines at the center
		start_x_0 = (width // 2) - (len(startup_str[0]) // 2 - 1)
		start_x_1 = (width // 2) - (len(startup_str[1]) // 2 - 1)
		start_x_2 = (width // 2) - (len(startup_str[2]) // 2 - 1)

		self.win_definition.addstr(start_y, start_x_0, startup_str[0])
		self.win_definition.addstr(start_y+1, start_x_1, startup_str[1])
		self.win_definition.addstr(start_y+2, start_x_2, startup_str[2])
		self.win_definition.refresh()
		self.win_input.move(1, 2) # init cursor position at startup

		# init wordlist
		self.redraw_wordlist_on_completion()

	def _print_key(self, key):
		""" print key for debugging purpose """
		height, _ = self.win_definition.getmaxyx()
		if key == "\n":
			return
		# clear last key pressed
		self.win_definition.hline(height - 2, 2, ' ', len('Key pressed: ') + 10)
		self.win_definition.addstr(height - 2, 2, 'Key pressed: ' + str(key))
		self.win_definition.refresh()

	def redraw_ui(self):
		""" redraw the whole window """
		self.stdscr.erase() # use erase() instead of clear() to avoid flickering
		# code
		self.stdscr.refresh()

		self.redraw_wordlist()
		self.redraw_input()
		self.redraw_definition()

	def redraw_wordlist(self):
		""" redraw wordlist window """
		# height, width = self.win_wordlist.getmaxyx()

		self.win_wordlist.erase()
		self.win_wordlist.box()
		for height, word in enumerate(self.completion):
			self.win_wordlist.addstr(height + 1, 2, word, curses.color_pair(2))
		self.win_wordlist.refresh()

	def redraw_input(self):
		""" redraw input bar window """
		height, width = self.win_input.getmaxyx()

		self.win_input.erase()
		self.win_input.box()
		start = len(self.input_buffer) - (width - 4)
		if start < 0:
			start = 0
		self.win_input.addstr(1, 2, self.input_buffer[start:], curses.color_pair(3))
		self.win_input.refresh()

	def redraw_definition(self):
		""" redraw definition window """
		height, width = self.win_definition.getmaxyx()

		self.win_definition.erase()
		self.win_definition.box()
		self.win_definition.refresh()

	def resize(self):
		""" handle window size when terminal size change """
		self.redraw_ui()
		raise 'resize'

	def redraw_wordlist_on_completion(self):
		""" update wordlist based on current self.input_buffer """
		height, _ = self.win_wordlist.getmaxyx()
		self.completion = self.get_completion_list(self.input_buffer, height - 2)
		self.redraw_wordlist()

	def redraw_wordlist_on_scrolling(self, direction): # temporary
		""" navigate wordlist by pressing up and down """
		height, _ = self.win_wordlist.getmaxyx()

		if direction == self.KEY_UP:
			self.completion_start_idx -= 1
		elif direction == self.KEY_DOWN:
			self.completion_start_idx += 1

		self.completion_start_idx = max(self.completion_start_idx, 0)
		self.completion = self.words[
				self.completion_start_idx:self.completion_start_idx + height - 2]
		self.redraw_wordlist()

	def display_definition(self):
		""" show definition of query after hit ENTER key """
		self.redraw_definition()
		if binary_search(self.input_buffer, self.words) == -1:
			self.display_suggested_words()
		else:
			self.win_definition.addstr(1, 2, '<Definition of {}>'.format(self.input_buffer))
		self.win_definition.refresh()

	def display_suggested_words(self):
		""" display list of word similar to the word just entered but not found """
		candidates = self.get_related_words(self.input_buffer)
		height, _ = self.win_definition.getmaxyx()
		if self.input_buffer in candidates: # cant find alternative words for suggestion
			self.win_definition.addstr(1, 2, 'No match found for "{}"'.format(self.input_buffer))
			return
		self.win_definition.addstr(1, 2,
				'No match found for "{}". Did you mean:'.format(self.input_buffer))
		for i, candidate in enumerate(candidates):
			if 2 + i > height - 2:
				return
			self.win_definition.addstr(2 + i, 2, ' * ' + candidate)

def main(stdscr):
	words = get_word_list()
	vocabulary = []
	for word in words:
		vocabulary.append(Word(word))
	program_ui = DictionaryUI(stdscr, vocabulary)
	program_ui.run()

if __name__ == '__main__':
	os.environ.setdefault('ESCDELAY', '0') # fix <Esc> key delay in curses
	curses.wrapper(main)

# vim: nofoldenable
