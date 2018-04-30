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
