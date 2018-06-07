using EDictionary.Core.Extensions;
using EDictionary.Vendors.RTF;

namespace EDictionary.Core.Models.WordComponents
{
	public class Pronunciation
	{
		public string Prefix { get; set; }
		public string Ipa { get; set; }
		public string IpaText => Ipa != null ? "/" + Ipa + "/" : "";
		public string Filename { get; set; }
	}
}