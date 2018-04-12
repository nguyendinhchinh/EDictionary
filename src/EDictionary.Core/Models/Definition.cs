using EDictionary.Core.Extensions;
using System;
using System.Linq;
using System.Text;

namespace EDictionary.Core.Models
{
	public class Definition
	{
		public string Property { get; set; }
		public string Label { get; set; }
		public string Refer { get; set; }
		public Reference[] References { get; set; }
		public string definition { get; set; }
		public string[] Examples { get; set; }

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("  ");

			if (Label != null)
				builder.Append(Label + " ");

			if (Refer != null)
				builder.Append(Refer + " ");

			if (Property != null)
				builder.Append(Property + " ");

			builder.AppendLine(definition);

			if (Examples != null)
				builder.AppendLine("    " + string.Join(Environment.NewLine + "    ", Examples));

			if (References != null)
				builder.Append("    " + string.Join(", ", References.Select(x => x.ToString())));

			return builder.ToString();
		}
	}
}
