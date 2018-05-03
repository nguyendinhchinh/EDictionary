﻿using EDictionary.Core.Models;
using EDictionary.Core.ViewModels;
using System;
using System.Windows.Input;

namespace EDictionary.Core.Commands
{
	public class PlayNAmEAudioCommand : ICommand
	{
		private EDictionaryViewModel viewModel;

		public PlayNAmEAudioCommand(EDictionaryViewModel viewModel)
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
			return viewModel.CanPlayAudio();
		}

		public void Execute(object parameter)
		{
			viewModel.PlayAudio(Dialect.NAmE);
		}
	}
}
