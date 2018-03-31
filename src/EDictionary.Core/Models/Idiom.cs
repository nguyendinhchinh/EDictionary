using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Models
{
	public class Idiom
	{
		public string idiom { get; set; }
		public Definition[] Definitions { get; set; } // Idiom Definitions dont have Property
	}
}
