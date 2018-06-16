using EDictionary.Core.Learner.Utilities;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDictionary.Core.Learner.ViewModels
{
	public partial class TaskIconViewModel
	{
		#region Fields

		private DefinitionViewModel dynamicVM;

		private GlobalKeyboardHook keyboardHook;
		private GlobalMouseHook mouseHook;
		private ClipboardManager clipboardManager;

		private bool autoCopyToClipboard;
		private bool useTriggerKey;
		private Keys triggerKey;

		#endregion

		#region Properties

		public DefinitionViewModel DynamicVM
		{
			get { return dynamicVM; }
			protected set { SetPropertyAndNotify(ref dynamicVM, value); }
		}

		#endregion

		private void InitDynamic()
		{
			DynamicVM = new DefinitionViewModel(wordLogic);
			DynamicVM.DoubleClickCommand = new DelegateCommand(SearchFromSelection);

			keyboardHook = new GlobalKeyboardHook();

			mouseHook = new GlobalMouseHook();
			mouseHook.DoubleClick += OnMouseDoubleClicked;

			clipboardManager = new ClipboardManager();
		}

		private async void OnMouseDoubleClicked(object sender, MouseEventArgs e)
		{
			if (autoCopyToClipboard)
				await SendCopyCommandAsync();

			if (useTriggerKey)
				keyboardHook.KeyPressed += OnKeyPressed;
			else
				OpenPopupFromClipboard();

			System.Diagnostics.Debug.WriteLine(clipboardManager.GetCurrentText());
		}

		private void OnKeyPressed(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != triggerKey)
				return;

			OpenPopupFromClipboard();

			keyboardHook.KeyUp -= OnKeyPressed;
		}

		private void OpenPopupFromClipboard()
		{
			if (clipboardManager.IsContainsText())
			{
				var selectedText = clipboardManager.GetCurrentText();

				if (selectedText.Split(' ').Length > 1)
					return;

				var word = wordLogic.Search(selectedText);

				if (word == null)
					return;

				DynamicVM.Word = word;
				DynamicVM.Definition = word.ToDisplayedString();

				OpenDynamicPopup();

				clipboardManager.Clear();
			}
		}

		private Task SendCopyCommandAsync()
		{
			var thread = new Thread(() =>
			{
				SendKeys.SendWait("^c");
				SendKeys.Flush();
			});

			thread.SetApartmentState(ApartmentState.MTA);
			thread.Start();
			thread.Join();

			return Task.FromResult(0);
		}

		/// <summary>
		/// Called when selecting a word in definition text area
		/// </summary>
		public void SearchFromSelection()
		{
			Word word = wordLogic.Search(DynamicVM.SelectedWord);

			if (word == null)
			{
				var stemmedWord = Stemmer.Stem(DynamicVM.SelectedWord);

				if (DynamicVM.SelectedWord != stemmedWord)
					word = wordLogic.Search(stemmedWord);
			}

			if (word != null)
				DynamicVM.SetContent(word);
			else
				DynamicVM.SetContent(wordLogic.GetSuggestions(DynamicVM.SelectedWord));
		}
	}
}
