using EDictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Extensions
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder AppendReferences(this StringBuilder builder, Reference[] references)
		{
			if (references != null)
			{
				builder.AppendLine("    " + string.Join(", ", references.Select(x => x.ToString())));
			}

			return builder;
		}

		public static StringBuilder AppendIfExists(this StringBuilder builder, string value)
		{
			if (value != null)
			{
				builder.Append(value);
			}

			return builder;
		}

		public static StringBuilder AppendLineIfExists(this StringBuilder builder, string value)
		{
			if (value != null)
			{
				builder.AppendLine(value);
			}

			return builder;
		}
		public static StringBuilder AppendObjs<T>(this StringBuilder builder, T[] objects)
		{
			builder.Append(string.Join(Environment.NewLine, objects.Select(x => x.ToString())));

			return builder;
		}

		public static StringBuilder AppendDefinitions(this StringBuilder builder, Definition[] definitions)
		{
			builder.Append(string.Join(Environment.NewLine, definitions.Select(x => x.ToString())));

			return builder;
		}

		public static StringBuilder AppendExamples(this StringBuilder builder, string[] examples)
		{
			if (examples != null && examples.Length != 0)
				builder.AppendLine("    " + string.Join(Environment.NewLine + "    ", examples));

			return builder;
		}

		public static StringBuilder AppendDefinitionsExamples(this StringBuilder builder, DefinitionGroup[] definitionsExamples)
		{
			if (definitionsExamples != null && definitionsExamples.Length != 0)
			{
				builder.Append(string.Join(Environment.NewLine, definitionsExamples.Select(x => x.ToString())));
			}

			return builder;
		}

		public static StringBuilder AppendExtraExamples(this StringBuilder builder, string[] extraExamples)
		{
			if (extraExamples != null && extraExamples.Length != 0)
			{
				string bullet = "* ";

				builder.AppendLine();
				builder.Append(bullet);
				builder.AppendLine(string.Join(Environment.NewLine + bullet, extraExamples));
			}

			return builder;
		}

		public static StringBuilder AppendIdioms(this StringBuilder builder, Idiom[] idioms)
		{
			if (idioms != null && idioms.Length != 0)
				builder.Append(string.Join("", idioms.Select(x => x.ToString())));

			return builder;
		}
	}
}
