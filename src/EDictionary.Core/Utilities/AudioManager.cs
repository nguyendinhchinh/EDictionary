using WMPLib;

namespace EDictionary.Core.Utilities
{
	public class AudioManager
	{
		private WindowsMediaPlayer wplayer;

		public AudioManager()
		{
			wplayer = new WindowsMediaPlayer();
		}

		public void Play(string audioPath)
		{
			wplayer.URL = audioPath;
			wplayer.controls.play();
		}
	}
}
