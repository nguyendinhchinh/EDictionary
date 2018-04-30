using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace EDictionary.Controls
{
	public class SelectionRichTextBox : RichTextBox
	{
		public SelectionRichTextBox()
		{
			// Use base class style
			SetResourceReference(StyleProperty, typeof(RichTextBox));
		}

		/// <summary>
		/// RichTextBox in WPF toolkit have Selection property. But it's readonly and due
		/// to WPF limitation, it cannot be binded even with OneWayToSource mode. SelectedText
		/// is basically Selection.Text but bindable
		/// </summary>
		public static readonly DependencyProperty SelectedWordProperty =
			DependencyProperty.Register(
					"SelectedWord",
					typeof(string),
					typeof(SelectionRichTextBox),
					new PropertyMetadata("")
					);

		public string SelectedWord
		{
			get
			{
				return (string)GetValue(SelectedWordProperty);
			}
			set
			{
				SetValue(SelectedWordProperty, value);
			}
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			TextPointer cursorPosition = CaretPosition;

			string strBeforeCursor = cursorPosition.GetTextInRun(LogicalDirection.Backward);
			string strAfterCursor = cursorPosition.GetTextInRun(LogicalDirection.Forward);

			string wordBeforeCursor = strBeforeCursor.Split().Last();
			string wordAfterCursor = strAfterCursor.Split().First();

			string text = wordBeforeCursor + wordAfterCursor;

			SelectedWord = string.Join("", text
				.Where(c => char.IsLetter(c))
				.ToArray());

			base.OnMouseUp(e);
		}
	}
}
