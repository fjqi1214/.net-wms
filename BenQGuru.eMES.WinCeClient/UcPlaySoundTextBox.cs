using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class UcPlaySoundTextBox : System.Windows.Forms.TextBox
    {
        public UcPlaySoundTextBox()
        {
            InitializeComponent();
        }


        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                SoundPlayer.PlayErrorMusic();
            }
        }

        public void SuccessMsgAndPlaySound(string successMsg)
        {
            base.Text = successMsg;
            SoundPlayer.PlaySuccessMusic();
        }
    }

    public class SoundPlayer
    {
        private enum Flags
        {
            SND_SYNC = 0x0000,  /* play synchronously (default) */
            SND_ASYNC = 0x0001,  /* play asynchronously */
            SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
            SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004  /* name is resource name or atom */
        }

        //wince调用的dll与win32不一致
        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySound(string szSound, IntPtr hMod, int flags);
        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySoundBytes(byte[] szSound, IntPtr hMod, int flags);


        private static void PlayMusic(string soundtype)
        {
            //wince路径
            string strPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            strPath = strPath.Substring(0, strPath.LastIndexOf(@"\"));

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

            if (files != null && files.Length > 0)
            {
                strPath += @"\" + soundtype + ".wav";
                if (System.IO.File.Exists(strPath))
                {
                    WCE_PlaySound(strPath, IntPtr.Zero, (int)(Flags.SND_FILENAME));
                }
            }
        }

        public static void PlayErrorMusic()
        {
            PlayMusic("Error");
        }

        public static void PlaySuccessMusic()
        {
            PlayMusic("Success");
        }
    }

}
