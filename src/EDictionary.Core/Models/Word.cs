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

			builder.AppendLine(word);
			builder.AppendLine(Pronunciations.ToString());

			if (References != null)
				builder.AppendLine("    " + string.Join(", ", References.Select(x => x.ToString())));

			builder.AppendLine(string.Join(Environment.NewLine, DefinitionsExamples.Select(x => x.ToString())));
			builder.AppendLine(string.Join(Environment.NewLine + "* ", ExtraExamples));
			builder.AppendLine();
			builder.Append(string.Join("", Idioms.Select(x => x.ToString())));

			return builder.ToString();
		}
	}
}
