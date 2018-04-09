namespace EDictionary.Core.Models
{
	public class Reference
	{
		public string Keyword { get; set; }
		public string Text { get; set; }

		public void print()
		{
			System.Console.WriteLine($"Keyword: {Keyword}");
			System.Console.WriteLine($"Text: {Text}");
		}
	}
}
