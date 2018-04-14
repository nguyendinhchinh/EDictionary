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

		public History()
		{
			history = new List<T> {};
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

		public T Previous()
		{
			if (currentIndex > 0)
			{
				return history[--currentIndex];
			}
			return history[0];
		}

		public T Next()
		{
			if (currentIndex < history.Count - 1)
			{
				return history[++currentIndex];
			}
			return history[history.Count - 1];
		}

        public T PreviousItem()
        {
            if (currentIndex > 0)
            {
                return history[currentIndex - 1];
            }
            return history[0];
        }

        public T NextItem()
        {
            if (currentIndex < history.Count - 1)
            {
                return history[currentIndex + 1];
            }
            return history[history.Count - 1];
        }

    }
}
