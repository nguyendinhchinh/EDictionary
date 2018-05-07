using EDictionary.Core.Extensions;
using EDictionary.Vendors.RTF;
using System.Drawing;

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

		public string ToRTFString()
		{
			RTFBuilder builder = new RTFBuilder();

			builder.AppendTitle(Name, Wordform);
			builder.AppendPronunciation(Pronunciations);
			builder.AppendReferences(References);
			builder.AppendDefinitionGroups(DefinitionsExamples);
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

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}
	}
}
