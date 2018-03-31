namespace EDictionary.Core.Models
{
	public class Idiom
	{
		public string idiom { get; set; }
		public Definition[] Definitions { get; set; } // Idiom Definitions dont have Property
	}
}
