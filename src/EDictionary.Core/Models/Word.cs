using EDictionary.Core.Extensions;
using System;
using System.Linq;
using System.Text;

namespace EDictionary.Core.Models
{
	public class Word
	{
		public string Id { get; set; }
		public string[] OtherKeywords { get; set; }
		public string Name { get; set; }
		public string Wordform { get; set; }
		public Pronunciation[] Pronunciations { get; set; }
		public Reference[] References { get; set; }
		public DefinitionGroup[] DefinitionsExamples { get; set; }
		public string[] ExtraExamples { get; set; }
		public Idiom[] Idioms { get; set; }

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLineIfExists(Name);
			builder.AppendObjs(Pronunciations);
			builder.AppendReferences(References);
			builder.AppendObjs(DefinitionsExamples);
			builder.AppendExtraExamples(ExtraExamples);
			builder.AppendIdioms(Idioms);

			return builder.ToString();
		}

		public override bool Equals(object obj)
		{
			Word word = obj as Word;

			if (this.Id == word.Id)
			{
				return true;
			}
			return false;
		}
	}
}
