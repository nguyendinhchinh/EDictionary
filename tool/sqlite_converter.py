#!/bin/env python

""" convert word data in json format to sqlite database """

import json
import os
import sqlite3

DB_PATH = os.path.join(os.getcwd(), '..\src\EDictionary\Data\words.sqlite')
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

			insert(json)

	CONNECTION.commit()

def main():
	""" main function """
	to_sqlite()

if __name__ == '__main__':
	main()

# vim: nofoldenable

