using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DevoPlayer
{
    class Track
    {
        public string filePath { get; protected set; }
        public string artist { get; protected set; }
        public string title { get; protected set; }
        public string length { get; protected set; }
        public DateTime dateAdded { get; protected set; }

        public Track(string filePath)
        {

            this.filePath = filePath;
            TagLib.File tagFile = TagLib.File.Create(filePath);

            extractArtist(tagFile);
            extractTitle(tagFile);
            extractLength(tagFile);
            dateAdded = DateTime.Now;

        }

        private void extractArtist(TagLib.File tagFile)
        {
            this.artist = "";
            // Check the tags
            foreach (String performer in tagFile.Tag.Performers)
            {
                this.artist += performer;
            }

            if (this.artist.Length < 1)
            {

                // If no tags, fall back to file name
                String fileName = System.IO.Path.GetFileNameWithoutExtension(this.filePath);

                int artistEndPos = fileName.LastIndexOf("-");
                if (artistEndPos < 0)
                {
                    this.artist = fileName;
                }
                else
                {
                    this.artist = fileName.Substring(0, artistEndPos);
                }
            }
        }

        private void extractTitle(TagLib.File tagFile)
        {
            

            String tagTitle = tagFile.Tag.Title;
            this.title = tagTitle;

            if (tagTitle.Length < 1)
            {
                // No title tag is found, so resort to file name.
                String fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                int titleStartPos = fileName.LastIndexOf("-");
                if (titleStartPos < 0)
                {
                    this.title = fileName;
                }
                else
                {
                    this.title = fileName.Substring(titleStartPos + 2); // +2 for the 2 spaces after the dash
                }
            }

        }

        private void extractTitleOld()
        {

            int titleStartPos = filePath.LastIndexOf("-");
            if (titleStartPos < 0)
            {
                titleStartPos = filePath.LastIndexOf("–");
                if (titleStartPos < 0)
                {
                    titleStartPos = filePath.LastIndexOf("\\");
                }
            }
            titleStartPos += 2; // to account for spaces
            string extension = ".mp3";
            int extensionPos = filePath.LastIndexOf(extension);
            int titleLength = extensionPos - titleStartPos;
            if (titleLength < 0)
            {
                titleLength = 0;
            }
            title = filePath.Substring(titleStartPos, titleLength);

        }

        private void extractLength(TagLib.File tagFile)
        {
            TimeSpan length = tagFile.Properties.Duration;
            if (length.Hours > 0)
            {
                this.length = String.Format("{0:hh\\:mm\\:ss}", length);
            }
            else
            {
                this.length = String.Format("{0:mm\\:ss}", length);
            }
        }


        public override string ToString()
        {
            return String.Format("Artist: {0}, Title: {1}, Duration: {2}, Date added: {3}",
                this.artist, this.title, this.length, this.dateAdded);
        }
    }
}
