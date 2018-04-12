using System;
using System.Linq;
using System.Text;

namespace EDictionary.Core.Models
{
	public class DefinitionGroup
	{
		public string Namespace { get; set; }
		public Definition[] Definitions { get; set; }

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine(">> " + Namespace);
			builder.Append(string.Join(Environment.NewLine, Definitions.Select(x => x.ToString())));

			return builder.ToString();
		}
	}
}
