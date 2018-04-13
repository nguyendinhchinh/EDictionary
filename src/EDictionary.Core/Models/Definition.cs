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
			builder.AppendIfExists(Label + " ");
			builder.AppendIfExists(Refer + " ");
			builder.AppendIfExists(Property + " ");
			builder.AppendLine(definition);
			builder.AppendExamples(Examples);
			builder.AppendReferences(References);

			return builder.ToString();
		}
	}
}
