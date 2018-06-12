using EDictionary.Core.Learner.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.Extensions
{
	public static class StringExtensions
	{
		public static Keys ToKey(this string str)
		{
			Keys key;

			var result = Enum.TryParse(str, ignoreCase: true, result: out key);

			if (result)
				return key;

			return Keys.None;
		}

		public static Modifiers ToModifier(this string str)
		{
			Modifiers modifier;

			var result = Enum.TryParse(str, ignoreCase: true, result: out modifier);

			if (result)
				return modifier;

			return Modifiers.None;
		}
	}
}
