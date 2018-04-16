#!/bin/env python

"""
Convert word data in json format to sqlite database

Some different before and after insert into db. See modify()

	* Change 'url' key to 'filename' in pronunciation which contain the filename
	only and trim the rest of the url

	* Change filename extension from ogg to mp3 (.NET cant play ogg natively)
"""

import json
import os
import sqlite3

DB_PATH = os.path.join(os.getcwd(), 'words.sqlite')
JSON_PATH = os.path.join(os.getcwd(), 'data\words')

CONNECTION = sqlite3.connect(DB_PATH)
CURSOR = CONNECTION.cursor()

TABLE_NAME = 'wordlist'

def readjson(path):
	""" return json path that hold data of word """
	with open(path, 'r') as file:
		return json.load(file)

def get_word(path):
	""" extract word name from json path """
	return path.split('.')[0]

def uglify(data):
	""" return a string of compressed json text """
	return json.dumps(data, separators=(',', ':'))

def modify(word):
	""" replace word urls with audio filenames """
	for i, _ in enumerate(word['pronunciations']):
		url = word['pronunciations'][i]['url']

		try:
			ogg_file = url.rsplit('/', 1)[1]
			mp3_file = ogg_file.split('.')[0] + '.mp3'

			word['pronunciations'][i]['filename'] = mp3_file
			word['pronunciations'][i].pop('url', None)
		except AttributeError: # NoneType has no attribute 'rsplit'
			pass

	return word

def create_table():
	""" create table if not exists """
	CURSOR.execute('CREATE TABLE IF NOT EXISTS {} (WordID NVARCHAR, Definition NVARCHAR)'
			.format(TABLE_NAME))

def insert(json):
	""" insert json data in db """
	word_id = json['id']
	json_str = uglify(json)

	CURSOR.execute('INSERT INTO {} (WordID, Definition) VALUES (?, ?)'.format(TABLE_NAME),
			(word_id, json_str))

def to_sqlite():
	""" get word info in json file from word name """
	create_table()

	for file in os.listdir(JSON_PATH):
		if file.endswith('.json'):
			word = get_word(file)
			print('inserting ' + word + ' data into db')

			path = os.path.join(JSON_PATH, file)
			json = readjson(path)

			insert(modify(json))

	CONNECTION.commit()

def main():
	""" main function """
	to_sqlite()

if __name__ == '__main__':
	main()

# vim: nofoldenable

