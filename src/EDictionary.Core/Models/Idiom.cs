using System;
using System.Linq;
using System.Text;

namespace EDictionary.Core.Models
{
	public class Idiom
	{
		public string idiom { get; set; }
		public Definition[] Definitions { get; set; } // Idiom Definitions dont have Property

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
				
			builder.AppendLine(">> " + idiom);
			builder.Append(string.Join(Environment.NewLine, Definitions.Select(x => x.ToString())));

			return builder.ToString();
		}
	}
}
