using EDictionary.Core.Extensions;
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

			builder.AppendLineIfExists(">> " + Namespace);
			builder.AppendDefinitions(Definitions);

			return builder.ToString();
		}
	}
}
