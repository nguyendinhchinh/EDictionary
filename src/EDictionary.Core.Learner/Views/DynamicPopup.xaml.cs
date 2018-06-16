using EDictionary.Core.ViewModels;
using System;
using System.Windows;

namespace EDictionary.Core.Learner.Views
{
	/// <summary>
	/// Interaction logic for DefinitionPopup.xaml
	/// </summary>
	public partial class DynamicPopup : Window
	{
		public DynamicPopup(ViewModelBase viewModel)
		{
			InitializeComponent();

			Deactivated += DynamicPopup_Deactivated;

			DataContext = viewModel;
		}

		private void DynamicPopup_Deactivated(object sender, EventArgs e)
		{
			//var popup = (DynamicPopup)sender;

			//App.Current.Dispatcher.Invoke(() =>
			//{
			//	popup.Close();
			//});
		}

		protected override void OnContentRendered(EventArgs e)
		{
			MoveBottomLeftEdgeOfWindowToMousePosition();

			base.OnContentRendered(e);
		}

		private void MoveBottomLeftEdgeOfWindowToMousePosition()
		{
			var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
			var mouse = transform.Transform(GetMousePosition());
			Left = mouse.X;
			Top = mouse.Y - ActualHeight;
		}

		public Point GetMousePosition()
		{
			System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
			return new Point(point.X, point.Y);
		}
	}
}
