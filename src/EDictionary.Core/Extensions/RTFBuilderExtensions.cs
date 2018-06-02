using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using EDictionary.Vendors.RTF;
using System.Drawing;
using System.Threading.Tasks;

namespace EDictionary.Core.Extensions
{
	public static class RTFBuilderExtensions
	{
		public static int titleFontSize = 40;
		public static int headerFontSize = 25;
		public static int defaultFontSize = 10;

		public static Color TitleColor = ColorPicker.Color("LightCyan");
		public static Color WordformColor = ColorPicker.Color("Cyan");
		public static Color HeadlineColor = ColorPicker.Color("Yellow");
		public static Color PronPrefixColor = ColorPicker.Color("LightMagenta");
		public static Color ExampleColor = ColorPicker.Color("LightBlue");
		public static Color LabelColor = ColorPicker.Color("Gray");

		public static RTFBuilder AppendTitle(this RTFBuilder builder, string word)
		{
			builder.FontSize(titleFontSize);
			builder.FontStyle(FontStyle.Bold);
			builder.ForeColor(TitleColor);
			builder.Append(word);

			return builder;
		}

		public static RTFBuilder AppendWordform(this RTFBuilder builder, string wordform)
		{
			builder.FontSize(headerFontSize);
			builder.ForeColor(WordformColor);
			builder.AppendLine("  " + wordform);

			builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendHeadline(this RTFBuilder builder, string str)
		{
			builder.FontSize(headerFontSize);
			builder.FontStyle(FontStyle.Bold);
			builder.ForeColor(HeadlineColor);

			builder.AppendLine(str);
			builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendPronunciation(this RTFBuilder builder, Pronunciation[] prons)
		{
			bool hasContent = false;

			foreach (var pron in prons)
			{
				if (pron.Ipa == null)
					continue;

				builder.ForeColor(PronPrefixColor);
				builder.FontStyle(FontStyle.Bold | FontStyle.Italic);
				builder.Append(pron.Prefix);

				builder.AppendLine($" /{pron.Ipa}/");

				hasContent = true;
			}

			if (hasContent)
				builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendReferences(this RTFBuilder builder, Reference[] references)
		{
			if (references == null)
				return builder;

			foreach (var reference in references)
				builder.AppendLine("🡒 " + reference.Name);

			builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendDefinitionGroups(this RTFBuilder builder, DefinitionGroup[] defGroups)
		{
			int nsIndex = 0;

			foreach (var group in defGroups)
			{
				if (group.Namespace != null)
					builder.AppendHeadline($"{++nsIndex}. {group.Namespace}");

				builder.AppendDefinitions(group.Definitions);
			}

			return builder;
		}

		public static RTFBuilder AppendDefinitions(this RTFBuilder builder, Definition[] definitions)
		{
			foreach (var definition in definitions)
				builder.AppendDefinition(definition);

			return builder;
		}

		public static RTFBuilder AppendDefinition(this RTFBuilder builder, Definition definition)
		{
			builder.Append("  ");

			if (definition.Label != null)
			{
				builder.FontStyle(FontStyle.Bold);
				builder.ForeColor(LabelColor);

				if (definition.Label[0] != '(')
					builder.Append($"({definition.Label}) ");
				else
					builder.Append(definition.Label + " ");
			}

			if (definition.Refer != null)
			{
				builder.ForeColor(LabelColor);
				builder.Append(definition.Refer);
			}

			if (definition.Property != null)
			{
				builder.ForeColor(LabelColor);
				builder.Append(definition.Property + " ");
			}

			if (definition.Description != null)
				builder.AppendLine(definition.Description);

			builder.AppendExamples(definition.Examples);
			builder.AppendReferences(definition.References);

			return builder;
		}

		public static RTFBuilder AppendExamples(this RTFBuilder builder, string[] examples)
		{
			if (examples.Length == 0)
				return builder;

			foreach (var example in examples)
			{
				builder.FontStyle(FontStyle.Italic);
				builder.ForeColor(ExampleColor);
				builder.AppendLine("  • " + example);
			}

			builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendExtraExamples(this RTFBuilder builder, string[] extraExamples)
		{
			if (extraExamples == null || extraExamples.Length == 0)
				return builder;

			builder.AppendHeadline("Other Examples");

			string bullet = " ◦ ";

			foreach (var example in extraExamples)
				builder.AppendLine(bullet + example);

			builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendIdioms(this RTFBuilder builder, Idiom[] idioms)
		{
			if (idioms == null || idioms.Length == 0)
				return builder;

			builder.AppendHeadline("Idioms");

			foreach (var idiom in idioms)
			{
				builder.FontStyle(FontStyle.Bold);
				builder.AppendLine(idiom.Name);
				builder.FontStyle(FontStyle.Regular);

				foreach (var definition in idiom.Definitions)
					builder.AppendDefinition(definition);
			}
			
			return builder;
		}
	}
}
