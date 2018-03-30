using System.IO;
using EDictionary.Core.Models;
using Newtonsoft.Json;

namespace EDictionary.Core.Utilities
{
	public static class Json
	{
		public static Word Read(string jsonFile)
		{
			using (StreamReader r = new StreamReader(jsonFile))
			{
				string strJson = r.ReadToEnd();
				return JsonConvert.DeserializeObject<Word>(strJson);
			}
		}
	}
}
