using EDictionary.Core.Learner.ViewModels.Interfaces;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using System;

namespace EDictionary.Core.Learner.ViewModels
{
	public class TaskbarIconViewModel : ViewModelBase, ILearnerViewModel
	{
		public Action ShowLearnerBalloonAction { get; set; }
		public Action HideLearnerBalloonAction { get; set; }

		public void OpenLearnerBalloon() => DispatchIfNecessary(ShowLearnerBalloonAction);
		public void CloseLearnerBalloon() => DispatchIfNecessary(HideLearnerBalloonAction);

		public TaskbarIconViewModel()
		{
			OpenLearnerBalloonCommand = new DelegateCommand(OpenLearnerBalloon);
			CloseLearnerBalloonCommand = new DelegateCommand(CloseLearnerBalloon);
		}

		public DelegateCommand OpenLearnerBalloonCommand { get; protected set; }
		public DelegateCommand CloseLearnerBalloonCommand { get; protected set; }
	}
}
