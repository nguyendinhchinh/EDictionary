#!/bin/env python

""" spellcheck for dictionary, run when input not found """

from util import get_word_list

class SpellCheck(object):
	""" Spellcheck word based on self.vocabulary """
	def __init__(self, source):
		self.vocabulary = set(source)

	def candidates(self, word):
		""" Generate possible spelling corrections for word. """
		return (self._known([word])
				or self._known(self._edits1(word))
				or self._known(self._edits2(word))
				or [word])

	def _known(self, words):
		""" The subset of `words` that appear in the vocabulary """
		return set(w for w in words if w in self.vocabulary)

	def _edits1(self, word):
		""" All edits that are one edit away from `word`. """
		letters   = 'abcdefghijklmnopqrstuvwxyz'
		splits    = [(word[:i], word[i:])    for i in range(len(word) + 1)]
		deletes   = [L + R[1:]               for L, R in splits if R]
		swap      = [L + R[1] + R[0] + R[2:] for L, R in splits if len(R) > 1]
		replaces  = [L + c + R[1:]           for L, R in splits if R for c in letters]
		inserts   = [L + c + R               for L, R in splits for c in letters]
		return (deletes + swap + replaces + inserts)

	def _edits2(self, word):
		""" All edits that are two edits away from `word`. """
		return (e2 for e1 in self._edits1(word) for e2 in self._edits1(e1))

def main():
	""" main function """
	english_words = get_word_list()
	spellcheck = SpellCheck(english_words)

	while True:
		wrong_word = input('Enter a wrong word: ')
		print('Candidates:')
		candidates = spellcheck.candidates(wrong_word)
		for candidate in candidates:
			print(candidate)

if __name__ == '__main__':
	main()

# vim: nofoldenable

