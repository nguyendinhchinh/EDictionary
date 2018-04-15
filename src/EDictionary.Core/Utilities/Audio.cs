using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Utilities
{
	public static class Audio
	{
		public static void Play(string audioPath)
		{
			WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

			wplayer.URL = audioPath;
			wplayer.controls.play();
		}
	}
}
