using EDictionary.Core.Extensions;
using System;
using System.Linq;
using System.Text;

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
			StringBuilder builder = new StringBuilder();

			builder.AppendLineIfExists(word);
			builder.AppendLine(Pronunciations.ToString());
			builder.AppendReferences(References);
			builder.AppendDefinitionsExamples(DefinitionsExamples);
			builder.AppendExtraExamples(ExtraExamples);
			builder.AppendIdioms(Idioms);

			return builder.ToString();
		}
	}
}
