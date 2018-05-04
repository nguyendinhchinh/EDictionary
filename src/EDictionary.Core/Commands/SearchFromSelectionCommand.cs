using EDictionary.Core.ViewModels;
using System;
using System.Windows.Input;

namespace EDictionary.Core.Commands
{
	public class SearchFromSelectionCommand : ICommand
	{
		private MainViewModel viewModel;

		public SearchFromSelectionCommand(MainViewModel viewModel)
		{
			this.viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return viewModel.CanSearchFromSelection();
		}

		public void Execute(object parameter)
		{
			viewModel.SearchFromSelection();
		}
	}
}
