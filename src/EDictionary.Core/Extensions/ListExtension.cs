using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.Extensions
{
	public static class ListExtension
	{
		public static T NextItem<T>(this List<T> list, int currentIndex)
		{
			if (currentIndex < 0 || currentIndex >= list.Count - 1)
				return default(T);

			return list[currentIndex + 1];
		}
	}
}
