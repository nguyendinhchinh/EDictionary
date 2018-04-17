using Iveonik.Stemmers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Utilities
{
	public static class Stemmer
	{
		private static IStemmer stemmer = new EnglishStemmer();

		public static string Stem(string word)
		{
			return stemmer.Stem(word);
		}
	}
}
