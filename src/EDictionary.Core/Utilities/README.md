## Table of Contents

* SpellCheck
	* [SpellCheck.Candidates()](https://github.com/NearHuscarl/E-Dictionary/tree/master/src/EDictionary.Core/Utilities#spellcheckcandidatesword)
	* [SpellCheck.ReadFromStdIn()](https://github.com/NearHuscarl/E-Dictionary/tree/master/src/EDictionary.Core/Utilities#spellcheckreadfromstdin)
* JsonHelper
	* [JsonHelper.Deserialize(strJson)](https://github.com/NearHuscarl/E-Dictionary/tree/master/src/EDictionary.Core/Utilities#jsonhelperdeserializestrjson)

## Functions

### `SpellCheck.Candidates(word)`

Parameters:

* `word`
	* Description: word to correct spelling
	* Optional: `false`
	* Type: `string`

Returns:

This function check if `word` is in `SpellCheck.Vocabulary` and return an `IEnumerable<string>`
of corrected words if `word` is wrong, otherwise it just returns `word`

### `SpellCheck.ReadFromStdIn()`

Purpose: Test `SpellCheck` class in console. Type a (wrong) word and see a list of possible correct words

Parameters:

N/A

Returns:

N/A

### `JsonHelper.Deserialize(strJson)`

Parameters:

* `strJson`
	* Description: json content in a string
	* Type: `string`
	* Optional: `false`

Returns:

This function return a value of type `Word` from json format. See [`models/word.cs`](https://github.com/NearHuscarl/E-Dictionary/blob/master/src/EDictionary.Core/Models/Word.cs)
