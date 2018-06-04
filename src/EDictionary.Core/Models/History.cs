using EDictionary.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	[Serializable]
	public class History<T>
	{
		[XmlIgnore]
		public static readonly string Directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

		[XmlIgnore]
		public static readonly string Path = System.IO.Path.Combine(Directory, "history.xml");

		public static readonly int MaxHistory = 1000;

		#region Properties

		public int CurrentIndex { get; set; }

		public List<T> Wordlist { get; set; }

		public T Current
		{
			get
			{
				if (CurrentIndex != -1)
					return Wordlist[CurrentIndex];
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
			get { return CurrentIndex == Wordlist.Count - 1; }
		}

		public int Count
		{
			get { return Wordlist.Count; }
		}

		#endregion

		#region Constructors

		public History()
		{
			Wordlist = new List<T> { };
			CurrentIndex = -1;
		}

		#endregion

		/// <summary>
		/// add history item at the next index and truncate the rest
		/// </summary>
		public void Add(T item)
		{
			if (CurrentIndex + 1 <= Wordlist.Count - 1)
			{
				Wordlist.Insert(CurrentIndex + 1, item);
			}
			else
			{
				Wordlist.Add(item);
			}

			CurrentIndex++;
			Wordlist = Wordlist.Take(CurrentIndex + 1).ToList();
		}

		public void Previous(out T item)
		{
			if (CurrentIndex > 0)
				item = Wordlist[--CurrentIndex];
			else
				item = Wordlist[0];
		}

		public void Next(out T item)
		{
			if (CurrentIndex < Wordlist.Count - 1)
				item = Wordlist[++CurrentIndex];
			else
				item = Wordlist[Wordlist.Count - 1];
		}

		/// <summary>
		/// Remove old history based on MaxHistory
		/// </summary>
		public void Trim()
		{
			if (Wordlist.Count > MaxHistory)
			{
				Wordlist = (List<T>)Wordlist.TakeLast(MaxHistory);
			}
		}
	}
}
