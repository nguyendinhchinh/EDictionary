using EDictionary.Core.Extensions;
using System;
using System.Text;

namespace EDictionary.Core.Models
{
	public class Pronunciations
	{
		public Britain Britain { get; set; }
		public America America { get; set; }

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendIfExists(Britain.Prefix + " ");
			builder.AppendLineIfExists(Britain.Ipa);
			builder.AppendIfExists(America.Prefix + " ");
			builder.AppendLineIfExists(America.Ipa);

			return builder.ToString();
		}
	}

	public abstract class Pronunciation
	{
		public string Prefix { get; set; }
		public string Ipa { get; set; }
		public string Url { get; set; }
	}

	public class Britain: Pronunciation {}
	public class America: Pronunciation {}
}
