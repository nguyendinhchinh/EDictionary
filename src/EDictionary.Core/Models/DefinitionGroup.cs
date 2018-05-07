using EDictionary.Core.Extensions;
using EDictionary.Vendors.RTF;

namespace EDictionary.Core.Models
{
	public class DefinitionGroup
	{
		public string Namespace { get; set; }
		public Definition[] Definitions { get; set; }
	}
}
