# EDictionary

[![BSD 3-Clause License](https://img.shields.io/badge/License-BSD_3--Clauses-blue.svg?longCache=true)](https://github.com/NearHuscarl/E-Dictionary/blob/master/LICENSE.md)
[![Version](https://img.shields.io/badge/Version-1.17.0-green.svg?longCache=true)](https://github.com/NearHuscarl/E-Dictionary/releases)

<p align="center">
  <img src="https://github.com/NearHuscarl/EDictionary/blob/master/screenshots/Logo.png"/>
</p>

EDitionary is a desktop application project for uni assignment written in WPF. It has basic feature (english translator) along with some add-ons to make learning English more convenient. Note: Some of the features is still in proof-of-concept state 

# Main Features

* Search 40k English words for definition
* Autocomplete when searching
* Words pronunciation
* Show other form of current word
* History
* EDictionary Learner: Periodically popup of a random word definition to help learning vocabulary
* EDictionary Dynamic: Search the currently selected text (by double click and press a trigger key)

# Screenshots

![Main window](https://github.com/NearHuscarl/EDictionary/blob/master/screenshots/Main.png)
![Learner](https://github.com/NearHuscarl/EDictionary/blob/master/screenshots/Learner.png)
![Dynamic](https://github.com/NearHuscarl/EDictionary/blob/master/screenshots/Dynamic.png)

# Development Environemnt

The project is written in .NET Framework 4.6 and C# 6.0. Compiled using Visual Studio 14

### Libraries Used
* [Newtonsoft.Json](https://www.newtonsoft.com/json)
* [System.Data.SQLite](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)
* [System.Data.SQLite.Core](https://www.nuget.org/packages/system.data.sqlite.core) (Need to fix a [sqlite-related error](https://stackoverflow.com/a/28092497/9449426))
* [StemmersNet](https://archive.codeplex.com/?p=stemmersnet)
* [FontAwesome.WPF](https://github.com/charri/Font-Awesome-WPF/blob/master/README-WPF.md)
* [NotifyIcon.Wpf](https://bitbucket.org/hardcodet/notifyicon-wpf)
* [avalonedit](http://avalonedit.net/)
* [MouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook)
* [LemmaGenerator](https://github.com/AlexPoint/LemmaGenerator)
