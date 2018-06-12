using EDictionary.Core.DataLogic;
using EDictionary.Core.Extensions;
using EDictionary.Core.Learner.Utilities;
using EDictionary.Core.Learner.ViewModels.Interfaces;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.ViewModels
{
	public class DynamicViewModel : TaskbarIconViewModel, IDynamicViewModel
	{
		private GlobalKeyboardHook keyboardHook;
		private SettingsLogic settingsLogic;

		private Modifiers modifierShortcut;
		private Keys keyShortcut;

		public DynamicViewModel()
		{
			settingsLogic = new SettingsLogic();

			keyboardHook = new GlobalKeyboardHook();

			keyboardHook.KeyPressedCallback += Callback;
		}

		private void Callback(KeyInfo keyInfo)
		{
			if (keyInfo.KeysHold.Contains((Keys)Modifiers.LControl))
				OpenLearnerBalloon();
		}

		private void AddKeyToHook(Modifiers modifier, Keys key)
		{
			keyboardHook.hookedKeys.Add((Keys)modifier);
			keyboardHook.hookedKeys.Add(key);
		}

		public void Setup()
		{
			Settings settings = settingsLogic.LoadSettings();

			modifierShortcut = (Modifiers)settings.ModifierShortcut.ToKey();
			keyShortcut = settings.KeyShortcut.ToKey();

			AddKeyToHook(modifierShortcut, keyShortcut);
			keyboardHook.Hook();
		}
	}
}
