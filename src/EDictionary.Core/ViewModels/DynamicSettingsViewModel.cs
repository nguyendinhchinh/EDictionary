using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.ViewModels
{
	public class DynamicSettingsViewModel : ViewModelBase, IDynamicSettingsViewModel
	{
		private List<string> modifierKeys;
		private string modifierShortcut;
		private string keyShortcut;

		public List<string> ModifierKeys
		{
			get { return modifierKeys; }
		}

		public string ModifierShortcut
		{
			get { return modifierShortcut; }
			set { SetPropertyAndNotify(ref modifierShortcut, value); }
		}

		public string KeyShortcut
		{
			get { return keyShortcut; }

			/// <summary>
			/// Limit to only one character (the newest). Cannot use property MaxLength = 1
			/// in Textbox because we want the user to update the key without having to press
			/// backspace
			/// </summary>
			set
			{
				string key = value;

				if (value.Length > 1)
				{
					key = key.ToCharArray()
						.Where(chr => chr != KeyShortcut.First())
						.Select(x => x)
						.First()
						.ToString();
				}

				SetPropertyAndNotify(ref keyShortcut, key.ToUpper());
			}
		}

		public DynamicSettingsViewModel()
		{
			modifierKeys = new List<string>()
			{
				"LControl",
				"RControl",
				"LAlt",
				"RAlt",
				"LWin",
				"RWin",
				"LShift",
				"RShift",
			};
		}
	}
}
