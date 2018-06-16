using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EDictionary.Core.ViewModels
{
	public class DynamicSettingsViewModel : ViewModelBase, IDynamicSettingsViewModel
	{
		private bool canEditTriggerKey;
		private bool autoCopyToClipboard;
		private bool useTriggerKey;
		private List<string> triggerKeys;
		private string selectedKey;

		public bool CanEditTriggerKey
		{
			get { return canEditTriggerKey; }
			set { SetPropertyAndNotify(ref canEditTriggerKey, value); }
		}

		public bool AutoCopyToClipboard
		{
			get { return autoCopyToClipboard; }
			set { SetPropertyAndNotify(ref autoCopyToClipboard, value); }
		}

		public bool UseTriggerKey
		{
			get { return useTriggerKey; }
			set
			{
				if (value)
					CanEditTriggerKey = true;
				else
					CanEditTriggerKey = false;

				SetPropertyAndNotify(ref useTriggerKey, value);
			}
		}

		public List<string> TriggerKeys
		{
			get { return triggerKeys; }
		}

		public string SelectedKey
		{
			get { return selectedKey; }
			set { SetPropertyAndNotify(ref selectedKey, value); }
		}

		public DynamicSettingsViewModel()
		{
			triggerKeys = new List<string>()
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
