using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace EDictionary.Core.Utilities
{
	public class WordContractResolver : DefaultContractResolver
	{
		private Dictionary<string, string> PropertyMappings { get; set; }

		public WordContractResolver()
		{
			PropertyMappings = new Dictionary<string, string> 
			{
				{"Similars", "similar"},
				{"DefinitionsExamples", "definitions"},
				{"ExtraExamples", "extra_examples"},
			};
		}

		protected override string ResolvePropertyName(string propertyName)
		{
			string resolvedName = null;

			PropertyMappings.TryGetValue(propertyName, out resolvedName);
			return resolvedName ?? base.ResolvePropertyName(propertyName);
		}
	}

}
