using System;

namespace BenQGuru.eMES.Web.Helper
{
	public class SoundPlayer
	{
		[System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
		public static extern bool PlaySound(string szSound, int hMod, int flags);
		
		private static int SND_FILENAME = 0x00020000;

		private static void PlayMusic(string soundtype)
		{
			string strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (strPath.EndsWith(@"\") == false)
				strPath += @"\";
			strPath += "Music";
			string[] files = null;
			if (System.IO.Directory.Exists(strPath))
			{
				files = System.IO.Directory.GetFiles(strPath, "*.wav");
			}
			else
			{
				System.IO.Directory.CreateDirectory(strPath);
			}

			if ( files!=null && files.Length>0 )
			{
				strPath += @"\" + soundtype +".wav";
				if(System.IO.File.Exists(strPath))
				{
					PlaySound(strPath, 0, SND_FILENAME);
				}
			}
		}

		public static void PlayErrorMusic()
		{
			PlayMusic(BenQGuru.eMES.Web.Helper.SoundType.Error);
		}

		public static void PlaySuccessMusic()
		{
			PlayMusic(BenQGuru.eMES.Web.Helper.SoundType.Success);
		}
	}
}
