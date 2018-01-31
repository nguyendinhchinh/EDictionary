#!/bin/env python

""" DictionaryUI module to run dictionary app in terminal """

import os
import curses
import textwrap

from spellcheck import SpellCheck
from util import get_word_list
from word import Word, WordList, Definition

class DictionaryCore(object):
	"""
	dictionary implementation.
	self.words: WordList (subclass of list that hold Word type) of words (Word type)
	"""
	def __init__(self, vocabulary):
		self.words = WordList(vocabulary)
		self.completion = []
		self.comp_pos = 0 # first completion index in self.words
		self.spellcheck = SpellCheck(self.get_wordlist())

	def _search_prefix(self, string, recursive=True):
		"""
		search words with matching prefix using binary search algorithm
		recursive=False: if string not found as a prefix of any word in sequence simply return -1
		recursive=True:  if string not found as a prefix of any word in sequence check if every
		prefix of that string match any prefix in sequence, if not again return -1
		"""
		min_pos = 0
		max_pos = len(self.words) - 1
		prefix_pos = -1
		while True:
			if max_pos < min_pos:
				if prefix_pos != -1:
					return prefix_pos
				if recursive is True:
					for i in range(len(string) + 1):
						temp_pos = self._search_prefix(string[:i], recursive=False)
						if temp_pos != -1:
							prefix_pos = temp_pos
						else:
							return prefix_pos
					return -1
				return -1
			cur_pos = (min_pos + max_pos) // 2
			if self.words[cur_pos].name.startswith(string):
				prefix_pos = cur_pos
			if self.words[cur_pos].name < string:
				min_pos = cur_pos + 1
			elif self.words[cur_pos].name > string:
				max_pos = cur_pos - 1
			elif self.words[cur_pos].name == string:
				return cur_pos

	def get_completion_list(self, substr, max=20):
		"""
		get list of n `max` number of suggestions for `substr` by
		searching for the longest prefix in self.words[:].name that contain `substr`
		"""
		self.comp_pos = self._search_prefix(substr)
		comp_list = []
		for i in range(max):
			if self.comp_pos + i < len(self.words):
				comp_list.append(self.words[self.comp_pos + i].name)
			else:
				return comp_list
		return comp_list

	def get_related_words(self, word):
		""" return a list of word similar to the word just entered but not found """
		return self.spellcheck.candidates(word)

	def get_wordlist(self):
		""" return a list of words (string) in the dictionary """
		return [word.name for word in self.words] # TODO: optimize

	def definition(self, word):
		"""
		get definition from a word (string)
		return None if word not in database (WordList)
		"""
		return self.words.definition(word)

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

	def __init__(self, stdscr, vocabulary):
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

		leftpanel_width = 25
		# create derived windows
		input_wrapper_hwyx = (3, leftpanel_width, 0, 0)
		input_hwyx = (input_wrapper_hwyx[0]-2, input_wrapper_hwyx[1]-2, 1, 2)
		wordlist_wrapper_hwyx = (curses.LINES-input_wrapper_hwyx[0], leftpanel_width,
				input_wrapper_hwyx[0], 0)
		wordlist_hwyx = (wordlist_wrapper_hwyx[0]-2, wordlist_wrapper_hwyx[1]-2, 1, 2)
		def_wrapper_hwyx = (curses.LINES, curses.COLS - wordlist_wrapper_hwyx[1],
				0, wordlist_wrapper_hwyx[1])
		def_hwyx = (def_wrapper_hwyx[0]-2, def_wrapper_hwyx[1]-2, 1, 2)

		# box drawing window that contain win_input
		self.win_input_wrapper = stdscr.derwin(*input_wrapper_hwyx)
		self.win_input = self.win_input_wrapper.derwin(*input_hwyx)
		self.win_wordlist_wrapper = stdscr.derwin(*wordlist_wrapper_hwyx)
		self.win_wordlist = self.win_wordlist_wrapper.derwin(*wordlist_hwyx)
		self.win_def_wrapper = stdscr.derwin(*def_wrapper_hwyx)
		self.win_def = self.win_def_wrapper.derwin(*def_hwyx)

		self.win_input.keypad(True) # special key will be interpreted by curses

		# set foreground to cyan
		self.win_input_wrapper.attrset(curses.color_pair(1))
		self.win_wordlist_wrapper.attrset(curses.color_pair(1))
		self.win_def_wrapper.attrset(curses.color_pair(1))
		self.win_input.attrset(curses.color_pair(3))
		self.win_wordlist.attrset(curses.color_pair(2))
		self.win_def.attrset(curses.color_pair(1))

		# # windows size variables
		# h, w = self.stdscr.getmaxyx()
		# h_i, w_i = self.win_input_wrapper.getmaxyx()
		# h_w, w_w = self.win_wordlist_wrapper.getmaxyx()
		# h_d, w_d = self.win_def_wrapper.getmaxyx()

		# # # input marker
		# self.win_input_wrapper.addch(0, 0, '*', curses.A_REVERSE)
		# self.win_input_wrapper.addch(h_i-1, 0, '*', curses.A_REVERSE)
		# self.win_input_wrapper.addch(0, w_i-1, '*', curses.A_REVERSE)
		# self.win_input_wrapper.insch(h_i-1, w_i-1, '*', curses.A_REVERSE)
		# # # wordlist marker
		# self.win_wordlist_wrapper.addch(0, 0, '*', curses.A_REVERSE)
		# self.win_wordlist_wrapper.addch(h_w-1, 0, '*', curses.A_REVERSE)
		# self.win_wordlist_wrapper.addch(0, w_w-1, '*', curses.A_REVERSE)
		# self.win_wordlist_wrapper.insch(h_w-1, w_w-1, '*', curses.A_REVERSE)
		# # # definition marker
		# self.win_def_wrapper.addch(0, 0, '*', curses.A_REVERSE)
		# self.win_def_wrapper.addch(h_d-1, 0, '*', curses.A_REVERSE)
		# self.win_def_wrapper.addch(0, w_d-1, '*', curses.A_REVERSE)
		# self.win_def_wrapper.insch(h_d-1, w_d-1, '*', curses.A_REVERSE)

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
		height, width = self.win_def.getmaxyx()
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

		self.win_def.addstr(start_y, start_x_0, startup_str[0])
		self.win_def.addstr(start_y+1, start_x_1, startup_str[1])
		self.win_def.addstr(start_y+2, start_x_2, startup_str[2])
		self.win_def.refresh()
		self.win_input.move(0, 0) # init cursor position at startup

		# init wordlist
		self.redraw_wordlist_on_completion()

	def _print_key(self, key):
		""" print key for debugging purpose """
		height, _ = self.win_def.getmaxyx()
		if key == "\n":
			return
		# clear last key pressed
		self.win_def.hline(height - 1, 0, ' ', len('Key pressed: ') + 10)
		self.win_def.addstr(height - 1, 0, 'Key pressed: ' + str(key))
		self.win_def.refresh()

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
		# height, width = self.win_wordlist_wrapper.getmaxyx()

		self.win_wordlist.erase()
		self.win_wordlist_wrapper.erase()
		self.win_wordlist_wrapper.box()
		for height, word in enumerate(self.completion):
			self.win_wordlist.addstr(height, 0, word)
		self.win_wordlist_wrapper.refresh()
		self.win_wordlist.refresh()

	def redraw_input(self):
		""" redraw input bar window """
		height, width = self.win_input_wrapper.getmaxyx()

		self.win_input.erase()
		self.win_input_wrapper.erase()
		self.win_input_wrapper.box()
		start = len(self.input_buffer) - (width - 4)
		if start < 0:
			start = 0
		self.win_input.addstr(self.input_buffer[start:])
		self.win_input_wrapper.refresh()
		self.win_input.refresh()

	def redraw_definition(self):
		""" redraw definition window """
		height, width = self.win_def_wrapper.getmaxyx()

		self.win_def.erase()
		self.win_def_wrapper.erase()
		self.win_def_wrapper.box()
		self.win_def.refresh()
		self.win_def_wrapper.refresh()

	def resize(self):
		""" handle window size when terminal size change """
		self.redraw_ui()
		raise 'resize'

	def redraw_wordlist_on_completion(self):
		""" update wordlist based on current self.input_buffer """
		height, _ = self.win_wordlist_wrapper.getmaxyx()
		self.completion = self.get_completion_list(self.input_buffer, height - 2)
		self.redraw_wordlist()

	def redraw_wordlist_on_scrolling(self, direction): # temporary
		""" navigate wordlist by pressing up and down """
		height, _ = self.win_wordlist_wrapper.getmaxyx()

		if direction == self.KEY_UP:
			self.comp_pos -= 1
		elif direction == self.KEY_DOWN:
			self.comp_pos += 1

		self.comp_pos = max(self.comp_pos, 0)
		self.completion = self.words[
				self.comp_pos:self.comp_pos + height - 2]
		self.redraw_wordlist()

	def display_definition(self):
		""" show definition of query after hit ENTER key """
		self.redraw_definition()
		if self.input_buffer not in self.words:
			self.display_suggested_words()
		else:
			self.win_def.addstr(self.definition(self.input_buffer))
		self.win_def.refresh()

	def display_suggested_words(self):
		""" display list of word similar to the word just entered but not found """
		candidates = self.get_related_words(self.input_buffer)
		height, _ = self.win_def.getmaxyx()
		self.win_def.addstr('No match found for "{}"'.format(self.input_buffer))
		if self.input_buffer in candidates: # cant find alternative words for suggestion
			return
		self.win_def.addstr('. Did you mean:')
		for i, candidate in enumerate(candidates):
			if 2 + i > height - 2:
				return
			self.win_def.addstr(1 + i, 0, ' * ' + candidate)

def main(stdscr):
	words = get_word_list()
	vocabulary = [Word(word, definition=None) for word in words]
	program_ui = DictionaryUI(stdscr, vocabulary)
	program_ui.run()

if __name__ == '__main__':
	os.environ.setdefault('ESCDELAY', '0') # fix <Esc> key delay in curses
	curses.wrapper(main)

# TODO:
# wrapper all window with a window wrapper that only contain border
# use a 'pad' for wordlist window

# vim: nofoldenable
