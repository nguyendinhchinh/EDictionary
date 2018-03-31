using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Models
{
	public class Word
	{
		public string Keyword { get; set; }
		public string[] OtherKeywords { get; set; }
		public string word { get; set; }
		public string Wordform { get; set; }
		public Pronunciations Pronunciations { get; set; }
		public Reference[] References { get; set; }
		public DefinitionGroup[] DefinitionsExamples { get; set; }
		public string[] ExtraExamples { get; set; }
		public Idiom[] Idioms { get; set; }

		public override string ToString()
		{
			return String.Format("{0}, {1}", word, Wordform);
		}
	}
}
