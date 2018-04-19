using EDictionary.Core.Extensions;
using System;
using System.Text;

namespace EDictionary.Core.Models
{
	public class Pronunciation
	{
		public string Prefix { get; set; }
		public string Ipa { get; set; }
		public string Filename { get; set; }

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendIfExists(Prefix + " ");
			builder.AppendLineIfExists(Ipa);

			return builder.ToString();
		}
	}
}