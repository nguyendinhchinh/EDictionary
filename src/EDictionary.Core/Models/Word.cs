using EDictionary.Core.Extensions;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using EDictionary.Vendors.RTF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Models
{
	public class Word
	{
		public static readonly string AudioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
		private static AudioManager audioPlayer = new AudioManager();

		public string ID { get; set; }
		public string[] Similars { get; set; }
		public string Name { get; set; }
		public string Wordform { get; set; }
		public Pronunciation[] Pronunciations { get; set; }
		public Reference[] References { get; set; }
		public DefinitionGroup[] DefinitionsExamples { get; set; }
		public string[] ExtraExamples { get; set; }
		public Idiom[] Idioms { get; set; }

		public string ToDisplayedString()
		{
			Watcher watch = new Watcher();

			List<Task<string>> tasks = new List<Task<string>>();

			watch.Print("[C] Init Build");

			tasks.Add(Task.Run(() => new StringBuilder().AppendReferences(References).ToString()));
			tasks.Add(Task.Run(() => new StringBuilder().AppendDefinitionGroups(DefinitionsExamples).ToString()));
			tasks.Add(Task.Run(() => new StringBuilder().AppendExtraExamples(ExtraExamples).ToString()));
			tasks.Add(Task.Run(() => new StringBuilder().AppendIdioms(Idioms).ToString()));

			Task.WaitAll(tasks.ToArray());

			watch.Print("[C] Build");

			StringBuilder builder = new StringBuilder();

			builder.AppendLine();

			foreach (var task in tasks)
			{
				builder.Append(task.Result);
			}

			watch.Print("[C] Add String");

			return builder.ToString();
		}

		[Obsolete]
		public string ToRTFString(bool mini=false)
		{
			Watcher watch = new Watcher();

			if (mini)
			{
				RTFBuilderExtensions.defaultFontSize = 18;
				RTFBuilderExtensions.titleFontSize = 22;
				RTFBuilderExtensions.headerFontSize = 20;
			}
			else
			{
				RTFBuilderExtensions.defaultFontSize = 25;
				RTFBuilderExtensions.titleFontSize = 40;
				RTFBuilderExtensions.headerFontSize = 28;
			}

			List<Task<string>> tasks = new List<Task<string>>();

			watch.Print("[C] Init Build");

			tasks.Add(Task.Run(() => new RTFBuilder().AppendTitle(Name).ToString()));
			tasks.Add(Task.Run(() => new RTFBuilder().AppendWordform(Wordform).ToString()));
			tasks.Add(Task.Run(() => new RTFBuilder().AppendPronunciation(Pronunciations).ToString()));
			tasks.Add(Task.Run(() => new RTFBuilder().AppendReferences(References).ToString()));
			tasks.Add(Task.Run(() => new RTFBuilder().AppendDefinitionGroups(DefinitionsExamples).ToString()));
			tasks.Add(Task.Run(() => new RTFBuilder().AppendExtraExamples(ExtraExamples).ToString()));
			tasks.Add(Task.Run(() => new RTFBuilder().AppendIdioms(Idioms).ToString()));

			Task.WaitAll(tasks.ToArray());

			watch.Print("[C] Build");

			RTFBuilder builder = new RTFBuilder();

			foreach (var task in tasks)
			{
				builder.AppendRTFDocument(task.Result);
			}

			watch.Print("[C] Add String");

			string str = builder.ToString();

			watch.Print("[C] ToString");

			return str;
		}

		private string GetFilename(Dialect dialect)
		{
			return Pronunciations
				.Where(x => x.Prefix == dialect.ToString())
				.Select(x => x.Filename)
				.First();
		}

		public void PlayAudio(Dialect dialect)
		{
			string filename = GetFilename(dialect);
			string audioFile = Path.Combine(AudioPath, filename);

			audioPlayer.Play(audioFile);
		}
	}
}
