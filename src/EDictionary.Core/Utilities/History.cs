using EDictionary.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Utilities
{
	public class History<T>
	{
		private List<T> history;
		private int currentIndex = -1;

		public T Current
		{
			get
			{
				if (currentIndex != -1)
					return history[currentIndex];
				else
					return default(T);
			}
		}

		public bool IsFirst
		{
			get
			{
				return currentIndex == 0;
			}
		}

		public bool IsLast
		{
			get
			{
				return currentIndex == history.Count - 1;
			}
		}

		public History()
		{
			history = new List<T> { };
		}

		/// <summary>
		/// add history item at the next index and truncate the rest
		/// </summary>
		public void Add(T item)
		{
			if (currentIndex + 1 <= history.Count - 1)
			{
				history.Insert(currentIndex + 1, item);
			}
			else
			{
				history.Add(item);
			}

			currentIndex++;
			history = history.Take(currentIndex + 1).ToList();
		}

		public void Previous(ref T item)
		{
			if (currentIndex > 0)
				item = history[--currentIndex];
			else
				item = history[0];
		}

		public void Next(ref T item)
		{
			if (currentIndex < history.Count - 1)
				item = history[++currentIndex];
			else
				item = history[history.Count - 1];
		}
	}
}
