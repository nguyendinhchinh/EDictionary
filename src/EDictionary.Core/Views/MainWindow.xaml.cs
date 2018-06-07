using EDictionary.Controls;
using EDictionary.Core.ViewModels.MainViewModel;
using EDictionary.Theme.Utilities;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ExtendedWindow
	{
		private MainViewModel viewModel;

		public MainWindow()
		{
			RegisterCustomHighlight();
			InitializeComponent();

			viewModel = new MainViewModel
			{
				ShowSettingsWindowAction = new Action(this.ShowSettingsWindow),
				ShowAboutWindowAction = new Action(this.ShowAboutWindow),
			};

			DataContext = viewModel;
		}

		private void RegisterCustomHighlight()
		{
			IHighlightingDefinition eDictionaryHighlighting;

			// Remember to set Build Action to 'Embedded Resource'
			using (Stream s = typeof(MainWindow).Assembly.GetManifestResourceStream("EDictionary.Core.Views.EDictionary.xshd"))
			{
				if (s == null)
					throw new InvalidOperationException("Could not find embedded resource");

				using (XmlReader reader = new XmlTextReader(s))
				{
					eDictionaryHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
						HighlightingLoader.Load(reader, HighlightingManager.Instance);
				}
			}

			// Apply custom colors
			var yellowHighlightingColor = eDictionaryHighlighting.NamedHighlightingColors.First(c => c.Name == "Yellow");
			var grayHighlightingColor = eDictionaryHighlighting.NamedHighlightingColors.First(c => c.Name == "Gray");
			var blueHighlightingColor = eDictionaryHighlighting.NamedHighlightingColors.First(c => c.Name == "Blue");

			// Change the Foreground Color
			yellowHighlightingColor.Foreground = new SimpleHighlightingBrush(ColorPicker.GetMediaColor("Yellow"));
			grayHighlightingColor.Foreground = new SimpleHighlightingBrush(ColorPicker.GetMediaColor("Gray"));
			blueHighlightingColor.Foreground = new SimpleHighlightingBrush(ColorPicker.GetMediaColor("LightBlue"));

			// and register it in the HighlightingManager
			HighlightingManager.Instance.RegisterHighlighting("EDictionary", new string[] { ".edic" }, eDictionaryHighlighting);
		}

		private void ShowSettingsWindow()
		{
			var settingsWindow = new SettingsWindow();

			// Make child window always on top of this window but not all other windows
			settingsWindow.Owner = this;
			settingsWindow.ShowDialog();
		}

		private void ShowAboutWindow()
		{
			var aboutWindow = new AboutWindow();

			aboutWindow.Owner = this;
			aboutWindow.ShowDialog();
		}
	}
}
