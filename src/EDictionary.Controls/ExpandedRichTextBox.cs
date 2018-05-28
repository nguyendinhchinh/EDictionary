using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace EDictionary.Controls
{
	public class ExpandedRichTextBox : Xceed.Wpf.Toolkit.RichTextBox
	{
		public ExpandedRichTextBox()
		{
			// Use base class style
			SetResourceReference(StyleProperty, typeof(Xceed.Wpf.Toolkit.RichTextBox));
		}

		#region SelectedWord DP
		/// <summary>
		/// RichTextBox in WPF toolkit have Selection property. But it's readonly and due
		/// to WPF limitation, it cannot be binded even with OneWayToSource mode. SelectedText
		/// is basically Selection.Text but bindable
		/// </summary>
		public static readonly DependencyProperty SelectedWordProperty =
			DependencyProperty.Register(
					"SelectedWord",
					typeof(string),
					typeof(ExpandedRichTextBox),
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

		#endregion

		/// <summary>
		/// Update SelectedWord property on mouse click
		/// </summary>
		/// <param name="e"></param>
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

		/// <summary>
		/// Change mouse cursor to hand if cursor is on text
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (GetCharAtCursorPosition(e) != '\0')
			{
				this.Cursor = Cursors.Hand;
			}
			else
			{
				this.Cursor = Cursors.Arrow;
			}

			base.OnMouseMove(e);
		}

		private char GetCharAtCursorPosition(MouseEventArgs e)
		{
			var mousePosition = e.GetPosition(this);

			TextPointer textPointer = GetPositionFromPoint(mousePosition, false);

			if (textPointer == null)
			{
				return default(char);
			}

			CaretPosition = textPointer;

			// Console.WriteLine(mousePosition);
			// Console.WriteLine(GetPositionFromPoint(mousePosition, false) == null);

			return CaretPosition.GetTextInRun(LogicalDirection.Forward).FirstOrDefault();
		}

		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			// Scroll to top when get new definition
			ScrollToHome();

			base.OnTextChanged(e);
		}
	}
}
