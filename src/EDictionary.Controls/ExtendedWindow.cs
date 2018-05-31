using EDictionary.Theme.Animations;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace EDictionary.Controls
{
	public class ExtendedWindow : Window
	{
		private WindowState lastState = WindowState.Normal;
		private DoubleAnimation openingAnimation;
		private DoubleAnimation closingAnimation;

		public ExtendedWindow()
		{
			this.Closing += OnWindowClosing;

			openingAnimation = WindowAnimations.FadeInAnimation;
			closingAnimation = WindowAnimations.FadeOutAnimation;
		}

		protected override void OnStateChanged(EventArgs e)
		{
			if (lastState == WindowState.Minimized && this.WindowState == WindowState.Normal)
			{
				BeginAnimation(OpacityProperty, openingAnimation);
			}

			lastState = this.WindowState;

			base.OnStateChanged(e);
		}

		private void OnWindowClosing(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.Closing -= OnWindowClosing;

			closingAnimation.Completed += (o, a) => this.Close();
			BeginAnimation(OpacityProperty, closingAnimation);
		}

		protected override void OnDeactivated(EventArgs e)
		{
			base.OnDeactivated(e);
		}
	}
}
