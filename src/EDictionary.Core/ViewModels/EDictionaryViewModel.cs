using EDictionary.Core.Commands;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EDictionary.Core.ViewModels
{
	public class EDictionaryViewModel : INotifyPropertyChanged
	{
		#region Fields

		private History<string> history;
		private Word word;
		private int selectedIndex;
		private string definition;

		#endregion

		#region Properties

		public Dictionary Dictionary { get; set; }
		public string CurrentWord { get; set; }

		public int SelectedIndex
		{
			get
			{
				return selectedIndex;
			}

			set
			{
				if (value != selectedIndex)
				{
					selectedIndex = value;
					NotifyPropertyChanged("SelectedIndex");
				}
			}
		}

		public string Definition
		{
			get
			{
				return definition;
			}

			set
			{
				if (value != definition)
				{
					definition = value;
					NotifyPropertyChanged("Definition");
				}
			}
		}

		#endregion
		
		#region Constructor

		public EDictionaryViewModel()
		{
			Dictionary = new Dictionary();
			history = new History<string>();

			SpellCheck.GetVocabulary(Dictionary.Wordlist);

			GoToDefinitionCommand = new GoToDefinitionCommand(this);
			UpdateWordlistIndexCommand = new UpdateWordlistIndexCommand(this);
		}

		#endregion

		#region INotifyPropertyChanged Implementation

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region Commands

		public ICommand GoToDefinitionCommand
		{
			get;
			private set;
		}

		public ICommand UpdateWordlistIndexCommand
		{
			get;
			private set;
		}

		#endregion

		#region Wordlist

		public void UpdateWordlistIndex()
		{
			SelectedIndex = Search.Prefix(CurrentWord, Dictionary.Wordlist);
		}

		public string CorrectWord(string word)
		{
			IEnumerable<string> candidates = SpellCheck.Candidates(word);
			return String.Join(Environment.NewLine, candidates);
		}

		#endregion

		#region definition utils

		/// <summary>
		/// Return Word object from data layer lookup
		/// </summary>
		private string GetDefinition(string wordStr)
		{
			return Dictionary.Search(wordStr)?.ToString();
		}

		private void UpdateHistory()
		{
			if (Dictionary.currentWord != null && Dictionary.currentWord.Id != history.Current)
				history.Add(Dictionary.currentWord.Id);
		}

		#endregion

		#region GoToDefinition

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void GoToDefinition()
		{
			Definition = GetDefinition(CurrentWord)
				?? GetDefinition(Stemmer.Stem(CurrentWord))
				?? CorrectWord(CurrentWord);

			UpdateHistory();
		}

		public bool CanGoToDefinition()
		{
			if (string.IsNullOrEmpty(CurrentWord))
				return false;

			return true;
		}

		#endregion

		#region JumpToDefinition

		/// <summary>
		/// Called when select highlight word in definition window
		/// Stem a word when word not found instead of running spellcheck
		/// </summary>
		public void JumpToDefinition(string word)
		{
			string definition = GetDefinition(word)
				?? GetDefinition(Stemmer.Stem(word));

			if (definition != null)
				Definition = definition;

			UpdateHistory();
		}

		public bool CanJumpToDefinition(string word)
		{
			if (string.IsNullOrEmpty(Definition))
				return false;

			return true;
		}

	   #endregion

		#region History

		public void NextHistory()
		{
			Definition = GetDefinition(history.Next());
		}

		public void PreviousHistory()
		{
			Definition = GetDefinition(history.Previous());
		}

		public bool CanPressNextHistory()
		{
			return !history.IsLast;
		}

		public bool CanPressPreviousHistory()
		{
			return !history.IsFirst;
		}

		#endregion
	}
}
