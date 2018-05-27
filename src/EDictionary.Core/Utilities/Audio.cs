using WMPLib;

namespace EDictionary.Core.Utilities
{
	public static class Audio
	{
		static WindowsMediaPlayer wplayer = new WindowsMediaPlayer();

		public static void Play(string audioPath)
		{

			wplayer.URL = audioPath;
			wplayer.controls.play();
		}
	}
}
