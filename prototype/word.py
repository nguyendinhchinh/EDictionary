#!/bin/env python

""" Word datatype that hold name (str) and definition (Definition) for dictionary app """

class Word(object):
	""" Word is used to store word value (string) and its definition (Definition) """
	def __init__(self, name, definition=None):
		self.name = name
		self.definition = definition

	def __str__(self):
		return 'Word ' + self.name

	def __repr__(self):
		return '(Word {}, Def: {})'.format(self.name, self.definition is not None)

	def get_definition(self):
		""" return definition of word if it's in the database """
		return '<Definition of {}>'.format(self.name) # placeholder

	def print_def(self):
		""" print def of Word """
		print(str(self.definition))

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

class WordList(list):
	"""
	WordList is a list which contain Word data type. The only different is the keyword 'in'
	use binary search algorithm because WordList element is always sorted and iterate through
	WordList[:].name instead of WordList[:]
	"""
	def __init__(self, words=None):
		super().__init__(words)
		for word in words:
			if not isinstance(word, Word):
				raise TypeError('{} is not type Word'.format(word))
		self._words = words if words is not None else list()

	def _binary_search(self, word):
		min_pos = 0
		max_pos = len(self._words) - 1
		while True:
			if max_pos < min_pos:
				return -1
			cur_pos = (min_pos + max_pos) // 2
			if self._words[cur_pos].name == word:
				return cur_pos
			elif self._words[cur_pos].name < word:
				min_pos = cur_pos + 1
			elif self._words[cur_pos].name > word:
				max_pos = cur_pos - 1

	def __contains__(self, word):
		if self._binary_search(word) == -1:
			return False
		return True

# vim: nofoldenable
