using EDictionary.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	public class History<T>
	{
		public static readonly string Directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
		public static readonly string Path = System.IO.Path.Combine(Directory, "history.xml");

		public static History<T> Default = new History<T>()
		{
			MaxHistory = 1000,
			CurrentIndex = -1,
		};

		#region Properties

		public int MaxHistory { get; set; }
		public int CurrentIndex { get; set; }

		public List<T> Collection { get; set; }

		public T Current
		{
			get
			{
				if (CurrentIndex != -1)
					return Collection[CurrentIndex];
				else
					return default(T);
			}
		}

		public bool IsFirst
		{
			get { return CurrentIndex == 0; }
		}

		public bool IsLast
		{
			get { return CurrentIndex == Collection.Count - 1; }
		}

		public int Count
		{
			get { return Collection.Count; }
		}

		#endregion

		#region Constructors

		public History()
		{
			Collection = new List<T>();
		}

		#endregion

		/// <summary>
		/// add history item at the next index and truncate the rest
		/// </summary>
		public void Add(T item)
		{
			if (CurrentIndex + 1 <= Collection.Count - 1)
			{
				Collection.Insert(CurrentIndex + 1, item);
			}
			else
			{
				Collection.Add(item);
			}

			CurrentIndex++;
			Collection = Collection.Take(CurrentIndex + 1).ToList();
		}

		public void Previous(out T item)
		{
			if (CurrentIndex > 0)
				item = Collection[--CurrentIndex];
			else
				item = Collection[0];
		}

		public void Next(out T item)
		{
			if (CurrentIndex < Collection.Count - 1)
				item = Collection[++CurrentIndex];
			else
				item = Collection[Collection.Count - 1];
		}

		/// <summary>
		/// Remove old history based on MaxHistory
		/// </summary>
		public void Trim()
		{
			if (Collection.Count > MaxHistory)
			{
				Collection = (List<T>)Collection.TakeLast(MaxHistory);
			}
		}
	}
}
