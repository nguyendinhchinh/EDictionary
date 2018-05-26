using EDictionary.Vendors.RTF;
using System;
using System.Linq;

namespace EDictionary.Core.Models.WordComponents
{
	public class Idiom
	{
		public string Name { get; set; }
		public Definition[] Definitions { get; set; } // Idiom Definitions dont have Property
	}
}
