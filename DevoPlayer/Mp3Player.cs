using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevoPlayer
{
    class Mp3Player : IDisposable
    {
        private bool debug = false;

        public string currentSongFilePath;
        public long currentSongLenght;
        public Form12 form;

        public Mp3Player(Form12 form) 
        {
            this.form = form;

        }

        public void Test()
        {
            Dispose();
            string file = @"D:\Dropbox\Music\General\3OH!3 - Don't Trust Me.mp3";
            Open(file);
            Play();
        }

        public void Open(string fileName)
        {
            if(currentSongFilePath != null)
            {
                Dispose();
            }
            currentSongFilePath = fileName;
            const string Format = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = String.Format(Format, currentSongFilePath);
            Send(command);
            currentSongLenght = getSongLength();
        }

        private long getSongLength()
        {
            StringBuilder sb = new StringBuilder(128);
            mciSendString("status MediaFile length", sb, 128, IntPtr.Zero);
            if(sb.ToString() == "")
            {
                return -1;
            }
            ulong songLength = Convert.ToUInt64(sb.ToString());
            return (long) songLength;
        }

 

        public void Play()
        {
            // Calls the WndProc from Form1.cs
            mciSendString("play MediaFile notify", null, 0, form.Handle);
        }

        public void Stop()
        {
            Dispose();
            Open(currentSongFilePath);
        }


        public void Pause()
        {
            string command = "pause MediaFile";
            Send(command);
        }

        public void Dispose()
        {
            string command = "close MediaFile";
            Send(command);
        }

        public void Seek(int pos)
        {

            string command = "seek MediaFile to " + pos;
            Send(command);
        }

        public long Position()
        {
            StringBuilder sb = new StringBuilder(128);
            mciSendString("status MediaFile position", sb, 128, IntPtr.Zero);
            if (sb.ToString() == "")
            {
                return -1;
            }
            ulong songPosition = Convert.ToUInt64(sb.ToString());
            return (long)songPosition;
        }

        public void SetVolume(int n)
        {
            string command = "setaudio MediaFile volume to " + n.ToString();
            Send(command);

        }

        private void Send(string command)
        {
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);


    }
}
