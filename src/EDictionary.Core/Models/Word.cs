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

	public class Pronunciations
	{
		public Britain Britain { get; set; }
		public America America { get; set; }
	}

	public class Geo
	{
		public string Prefix { get; set; }
		public string Ipa { get; set; }
		public string Url { get; set; }
	}

	public class Britain: Geo {}
	public class America: Geo {}

	public class DefinitionGroup
	{
		public string Namespace { get; set; }
		public Definition[] Definitions { get; set; }
	}

	public class Definition
	{
		public string Property { get; set; }
		public string Label { get; set; }
		public string Refer { get; set; }
		public Reference[] References { get; set; }
		public string definition { get; set; }
		public string[] Examples { get; set; }
	}

	public class Reference
	{
		public string Keyword { get; set; }
		public string Text { get; set; }
	}

	public class Idiom
	{
		public string idiom { get; set; }
		public Definition[] Definitions { get; set; } // Idiom Definitions dont have Property
	}
}
