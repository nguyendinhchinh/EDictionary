using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using EDictionary.Vendors.RTF;

namespace EDictionary.Core.Extensions
{
	public static class RTFBuilderExtensions
	{
		public static int titleFontSize = 40;
		public static int headerFontSize = 25;
		public static int defaultFontSize = 10;

		public static RTFBuilder AppendTitle(this RTFBuilder builder, string word)
		{
			builder.FontSize(titleFontSize);
			builder.FontStyle(System.Drawing.FontStyle.Bold);
			builder.ForeColor(ColorPicker.Color("LightCyan"));
			builder.Append(word);

			return builder;
		}

		public static RTFBuilder AppendWordform(this RTFBuilder builder, string wordform)
		{
			builder.FontSize(headerFontSize);
			builder.ForeColor(ColorPicker.Color("Cyan"));
			builder.AppendLine("  " + wordform);

			builder.AppendLine();

			return builder;
		}

		public static RTFBuilder AppendHeadline(this RTFBuilder builder, string str)
		{
			builder.FontSize(headerFontSize);
			builder.FontStyle(System.Drawing.FontStyle.Bold);
			builder.ForeColor(ColorPicker.Color("Yellow"));

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

				builder.ForeColor(ColorPicker.Color("LightMagenta"));
				builder.FontStyle(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
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
				builder.FontStyle(System.Drawing.FontStyle.Bold);
				builder.ForeColor(ColorPicker.Color("Gray"));

				if (definition.Label[0] != '(')
					builder.Append($"({definition.Label}) ");
				else
					builder.Append(definition.Label + " ");
			}

			if (definition.Refer != null)
			{
				builder.ForeColor(ColorPicker.Color("Gray"));
				builder.Append(definition.Refer);
			}

			if (definition.Property != null)
			{
				builder.ForeColor(ColorPicker.Color("Gray"));
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
				builder.FontStyle(System.Drawing.FontStyle.Italic);
				builder.ForeColor(ColorPicker.Color("LightBlue"));
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
				builder.FontStyle(System.Drawing.FontStyle.Bold);
				builder.AppendLine(idiom.Name);
				builder.FontStyle(System.Drawing.FontStyle.Regular);

				foreach (var definition in idiom.Definitions)
					builder.AppendDefinition(definition);
			}
			
			return builder;
		}
	}
}
