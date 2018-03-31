namespace EDictionary.Core.Models
{
	public class Pronunciations
	{
		public Britain Britain { get; set; }
		public America America { get; set; }
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
