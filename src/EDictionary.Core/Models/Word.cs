using EDictionary.Core.Extensions;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Vendors.RTF;
using System.Drawing;

namespace EDictionary.Core.Models
{
	public class Word
	{
		public string Id { get; set; }
		public string[] Similars { get; set; }
		public string Name { get; set; }
		public string Wordform { get; set; }
		public Pronunciation[] Pronunciations { get; set; }
		public Reference[] References { get; set; }
		public DefinitionGroup[] DefinitionsExamples { get; set; }
		public string[] ExtraExamples { get; set; }
		public Idiom[] Idioms { get; set; }

		public string ToRTFString(bool mini=false)
		{
			if (mini)
			{
				RTFBuilderExtensions.defaultFontSize = 18;
				RTFBuilderExtensions.titleFontSize = 22;
				RTFBuilderExtensions.headerFontSize = 20;
			}
			else
			{
				RTFBuilderExtensions.defaultFontSize = 25;
				RTFBuilderExtensions.titleFontSize = 40;
				RTFBuilderExtensions.headerFontSize = 28;
			}

			RTFBuilder builder = new RTFBuilder(defaultFontSize: RTFBuilderExtensions.defaultFontSize);

			builder.AppendTitle(Name);
			builder.AppendWordform(Wordform);
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
