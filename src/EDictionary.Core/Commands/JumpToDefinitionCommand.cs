using EDictionary.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EDictionary.Core.Commands
{
	class JumpToDefinitionCommand : ICommand
	{
		private EDictionaryViewModel viewModel;

		public JumpToDefinitionCommand(EDictionaryViewModel viewModel)
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
			return viewModel.CanJumpToDefinition();
		}

		public void Execute(object parameter)
		{
			viewModel.JumpToDefinition();
		}
	}
}
