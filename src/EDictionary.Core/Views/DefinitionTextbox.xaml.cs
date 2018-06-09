using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.DefinitionViewModel;
using System.Windows;
using System.Windows.Controls;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for DefinitionTextbox.xaml
	/// </summary>
	public partial class DefinitionTextbox : UserControl
	{
		public DefinitionTextbox()
		{
			InitializeComponent();
		}

		public int NameFontSize
		{
			get { return (int)GetValue(NameFontSizeProperty); }
			set { SetValue(NameFontSizeProperty, value); }
		}

		public static readonly DependencyProperty NameFontSizeProperty = DependencyProperty.Register(
			"NameFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int WordformFontSize
		{
			get { return (int)GetValue(WordformFontSizeProperty); }
			set { SetValue(WordformFontSizeProperty, value); }
		}

		public static readonly DependencyProperty WordformFontSizeProperty = DependencyProperty.Register(
			"WordformFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int AudioButtonSize
		{
			get { return (int)GetValue(AudioButtonSizeProperty); }
			set { SetValue(AudioButtonSizeProperty, value); }
		}

		public static readonly DependencyProperty AudioButtonSizeProperty = DependencyProperty.Register(
			"AudioButtonSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int PronunciationFontSize
		{
			get { return (int)GetValue(PronunciationFontSizeProperty); }
			set { SetValue(PronunciationFontSizeProperty, value); }
		}

		public static readonly DependencyProperty PronunciationFontSizeProperty = DependencyProperty.Register(
			"PronunciationFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));

		public int DefinitionFontSize
		{
			get { return (int)GetValue(DefinitionFontSizeProperty); }
			set { SetValue(DefinitionFontSizeProperty, value); }
		}

		public static readonly DependencyProperty DefinitionFontSizeProperty = DependencyProperty.Register(
			"DefinitionFontSize",
			typeof(int),
			typeof(DefinitionTextbox),
			new PropertyMetadata(10));
	}
}
