using System;
using System.Windows;
using System.Windows.Input;

namespace Delete.Styles.Utilities
{
	public class WindowMinimizeCommand : ICommand
	{

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			var window = parameter as Window;

			if (window != null)
			{
				// If you use customized window by setting WindowStyle to None, the
				// minimize button will still minimize the window but without the
				// minimize animation. Instead it just instantly hides the
				// window. This can be fix as a hack by setting WindowStyle to
				// normal (SingleBorderWindow) the moment before executing minimize
				// action on the window
				window.WindowStyle = WindowStyle.SingleBorderWindow;

				window.WindowState = WindowState.Minimized;
			}
		}
	}
}
