using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevoPlayer
{
    class CurrentTrack
    {
        


        public void Test()
        {
            string command = @"open ""D:\Dropbox\Music\General\3OH!3 - Don't Trust Me.mp3"" type mpegvideo alias MediaFile";
            mciSendString(command, null, 0, 0);
            command = "play Mediafile";
            mciSendString(command, null, 0, 0);
        }

        public void Open(string fileName)
        {

            const string Format = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = String.Format(Format, fileName);
            Send(command);
            Play();
        }

        public void Play()
        {
            string command = "play MediaFile";
            Send(command);
        }

        public void Pause()
        {
            string command = "stop MediaFile";
            Send(command);
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            string command = "close MediaFile";
            Send(command);
        }

        private void Send(string command)
        {
            mciSendString(command, null, 0, 0);
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwndCallback);
    }
}
