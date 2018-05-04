using EDictionary.Core.ViewModels;
using System;
using System.Windows.Input;

namespace EDictionary.Core.Commands
{
	public class SearchFromInputCommand : ICommand
	{
		private MainViewModel viewModel;

		public SearchFromInputCommand(MainViewModel viewModel)
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
			return viewModel.CanSearchFromInput();
		}

		public void Execute(object parameter)
		{
			viewModel.SearchFromInput();
		}
	}
}
