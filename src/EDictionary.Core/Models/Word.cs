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
		[JsonProperty(PropertyName = "keyword")]
		public string Keyword { get; set; }

		[JsonProperty(PropertyName = "key_word")]
		public string[] OtherKeywords { get; set; }

		[JsonProperty(PropertyName = "word")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "wordform")]
		public string Wordform { get; set; }

		[JsonProperty(PropertyName = "pronunciations")]
		public Pronunciations Pronunciations { get; set; }

		[JsonProperty(PropertyName = "preference")]
		public Preference[] Preferences { get; set; }

		[JsonProperty(PropertyName = "definitions_examples")]
		public DefGroup[] DefinitionsExamples { get; set; }

		[JsonProperty(PropertyName = "extra_examples")]
		public string[] ExtraExamples { get; set; }

		[JsonProperty(PropertyName = "idioms")]
		public Idiom[] Idioms { get; set; }
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

	public class DefGroup
	{
		public string Namespace { get; set; }
		public Def[] Definitions { get; set; }
	}

	public class Def
	{
		public string Property { get; set; }
		public string Label { get; set; }
		public string Refer { get; set; }
		public Preference[] Preferences { get; set; }
		public string Definition { get; set; }
		public string[] Examples { get; set; }
	}

	public class Preference
	{
		public string Keyword { get; set; }
		public string Text { get; set; }
	}

	public class Idiom
	{
		public string Name { get; set; }
		public Def[] Definitions { get; set; } // Idiom Definitions dont have Property
	}
}
