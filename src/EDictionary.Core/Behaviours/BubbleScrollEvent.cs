using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EDictionary.Core.Behaviours
{
	/// <summary>
	/// Child elements of scrollviewer preventing scrolling with mouse wheel
	/// https://stackoverflow.com/a/16110178/9449426
	/// </summary>
	public sealed class BubbleScrollEvent : Behavior<UIElement>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
			base.OnDetaching();
		}

		void AssociatedObject_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			e.Handled = true;
			var e2 = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
			e2.RoutedEvent = UIElement.MouseWheelEvent;
			AssociatedObject.RaiseEvent(e2);
		}
	}
}
