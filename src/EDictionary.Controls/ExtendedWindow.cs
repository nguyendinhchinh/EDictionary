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

		#region CloseTrigger DP

		public static readonly DependencyProperty CloseTriggerProperty = DependencyProperty.Register(
			"CloseTrigger", typeof(bool),
			typeof(ExtendedWindow),
			new PropertyMetadata(
				false,
				OnCloseTriggerChanged)
			);

		public bool CloseTrigger
		{
			get
			{
				return (bool)GetValue(CloseTriggerProperty);
			}
			set
			{
				SetValue(CloseTriggerProperty, value);
			}
		}

		private static void OnCloseTriggerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			ExtendedWindow window = source as ExtendedWindow;

			if (window == null)
				return;

			if (window.CloseTrigger)
			{
				window.Close();
			}
		}

		#endregion
	}
}
