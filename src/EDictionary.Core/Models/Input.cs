using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Models
{
	public class Input : INotifyPropertyChanged
	{
		private string currentWord;

		public string Text
		{
			get
			{
				return currentWord;
			}

			set
			{
				if (value != currentWord)
				{
					currentWord = value;
					NotifyPropertyChanged("Text");
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
