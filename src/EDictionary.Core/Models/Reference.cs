namespace EDictionary.Core.Models
{
	public class Reference
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
