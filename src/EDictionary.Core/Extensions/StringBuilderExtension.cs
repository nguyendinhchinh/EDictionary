using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Extensions
{
	public static class StringBuilderExtension
	{
		public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
		{
			if (condition)
			{
				builder.Append(value);
			}

			return builder;
		}

		public static StringBuilder AppendLineIf(this StringBuilder builder, bool condition, string value)
		{
			if (condition)
			{
				builder.AppendLine(value);
			}

			return builder;
		}
	}
}
