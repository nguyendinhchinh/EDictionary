#!/bin/env python

""" Word datatype that hold name (str) and definition (Definition) for dictionary app """

class Word(object):
	""" Word is used to store word value (string) and its definition (Definition) """
	def __init__(self, name, definition=None):
		self._name = name
		self._definition = definition

	def __str__(self):
		return 'Word ' + self._name

	def __repr__(self):
		return '(Word {}, Def: {})'.format(self._name, self._definition is not None)

	@property
	def definition(self):
		""" return definition of word if it's in the database """
		if self._definition is None:
			return '<Definition of {}>'.format(self._name) # placeholder
		return self._definition

	@property
	def name(self):
		""" get name value """
		return self._name

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
		return "verb:\n {}\n {}\nnoun\n {}\n {}adj\n {}\n {}adv\n {}\n {}".format(
				self.verb_meaning, self.verb_examples,
				self.noun_meaning, self.noun_examples,
				self.adj_meaning, self.adj_examples,
				self.adv_meaning, self.adv_examples
				)

class WordList(list):
	"""
	WordList is a list which contain Word data type. The only different is the keyword 'in'
	use binary search algorithm because WordList element is always sorted and iterate through
	WordList[:].name instead of WordList[:]
	"""
	def __init__(self, words=None):
		super().__init__(words)
		for word in words: # TODO: optimize
			if not isinstance(word, Word):
				raise TypeError('{} is not type Word'.format(word))
		self.words = words if words is not None else list()

	def _binary_search(self, word):
		min_pos = 0
		max_pos = len(self.words) - 1
		while True:
			if max_pos < min_pos:
				return -1
			cur_pos = (min_pos + max_pos) // 2
			if self.words[cur_pos].name == word:
				return cur_pos
			elif self.words[cur_pos].name < word:
				min_pos = cur_pos + 1
			elif self.words[cur_pos].name > word:
				max_pos = cur_pos - 1

	def __contains__(self, word):
		if self._binary_search(word) == -1:
			return False
		return True

	def index(self, word):
		"""
		override list.index() to return index from `word`
		which will be searched in WordList[:].name
		return -1 if `word` not found instead of throwing error
		"""
		return self._binary_search(word)

	def definition(self, word):
		""" return definition of `word` """
		pos = self.index(word)
		return self.words[pos].definition

# vim: nofoldenable
