using Iveonik.Stemmers;

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
