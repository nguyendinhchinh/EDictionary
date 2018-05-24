using EDictionary.Core.Models;
using System;
using System.ComponentModel;

namespace EDictionary.Core.ViewModels
{
	public class PopupViewModel : ViewModelBase
	{
		#region Fields

		private string definition;

		#endregion

		#region Properties

		public string Definition
		{
			get
			{
				return definition;
			}

			set
			{
				SetPropertyAndNotify(ref definition, value);
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// About screen:
		///  Author: 3
		///  License: BSD 3-Clauses
		///  Version: Asembly
		///
		/// Seting screen:
		///  Checkbox: richtextbox format (default true)
		///  Interval: time to popup
		///  Radio: popup all words or
		///  
		///  Textbox: enter list of word to track
		///  Listview: list of word to track
		///
		/// Help screen:
		///  EDictionary is ...
		///
		/// Setting help screen
		///  Interval is...
		///  richtextbox format is ...
		///  
		/// </summary>
		public PopupViewModel()
		{
			EDict dictionary = new EDict();
			Random random = new Random();

			var randWord = dictionary.WordList[random.Next(dictionary.WordList.Count)];

			Definition = dictionary.Search(randWord).ToRTFString();
		}

		#endregion
	}
}
