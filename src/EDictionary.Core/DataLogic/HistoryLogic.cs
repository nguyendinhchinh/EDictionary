﻿using EDictionary.Core.Data;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System.Threading.Tasks;

namespace EDictionary.Core.DataLogic
{
	public class HistoryLogic
	{
		private HistoryAccess historyAccess;

		public HistoryLogic()
		{
			historyAccess = new HistoryAccess();
		}

		public void SaveHistory<T>(History<T> history)
		{
			history.Trim();

			historyAccess.SaveHistory(history);
		}

		public History<T> LoadHistory<T>()
		{
			var result = historyAccess.LoadHistory<T>();

			if (result.Status == Status.Success)
			{
				return result.Data;
			}

			return new History<T>();
		}

		public async Task<History<T>> LoadHistoryAsync<T>()
		{
			return await Task.Run(() => this.LoadHistory<T>());
		}

		public async Task SaveHistoryAsync<T>(History<T> history)
		{
			await Task.Run(() => this.SaveHistory<T>(history));
		}
	}
}
