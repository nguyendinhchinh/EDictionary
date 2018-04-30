﻿using EDictionary.Core.ViewModels;
using System;
using System.Windows.Input;

namespace EDictionary.Core.Commands
{
	public class NextHistoryCommand : ICommand
	{
		private EDictionaryViewModel viewModel;

		public NextHistoryCommand(EDictionaryViewModel viewModel)
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
			return viewModel.CanGoToNextHistory();
		}

		public void Execute(object parameter)
		{
			viewModel.NextHistory();
		}
	}
}