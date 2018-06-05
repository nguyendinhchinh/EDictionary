using EDictionary.Core.Data;
using EDictionary.Core.Data.Factory;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDictionary.Core.DataLogic
{
	public class HistoryLogic
	{
		private HistoryAccess historyAccess;

		public HistoryLogic()
		{
			historyAccess = DatabaseFactory.CreateHistoryDatabase();
		}

		public void UpdateCurrentIndex(int currentIndex)
		{
			historyAccess.UpdateCurrentIndex(currentIndex);
		}

		public void UpdateMaxHistory(int maxHistory)
		{
			historyAccess.UpdateMaxHistory(maxHistory);
		}

		public void AddItem<T>(T item)
		{
			historyAccess.InsertItem(item);
		}

		private List<T> LoadHistoryCollection<T>()
		{
			var result = historyAccess.SelectCollection<T>();

			if (result.Status == Status.Success)
			{
				return result.Data;
			}

			return new List<T>();
		}

		public History<T> LoadHistory<T>()
		{
			var result = historyAccess.SelectParameters<T>();

			if (result.Status == Status.Success)
			{
				History<T> history = result.Data;
				history.Collection = LoadHistoryCollection<T>();

				if (history.Collection.Count > history.MaxHistory)
				{
					history.Trim();
					historyAccess.DeleteFirstNRows(history.Collection.Count - history.MaxHistory);
				}

				return history;
			}

			return History<T>.Default;
		}

		public async Task<History<T>> LoadHistoryAsync<T>()
		{
			return await Task.Run(() => this.LoadHistory<T>());
		}
	}
}
