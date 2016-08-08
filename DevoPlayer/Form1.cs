using DevoPlayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
/*
** TO DO
** List of random songs played
** Favorites list (song rankings)
** Lyrics scrubbing
** Improve playlist graphics
** Playlist sorting
** Auto-add songs from folder
** Playlist tabs
** Playlist class
*/


namespace DevoPlayer
{
    public partial class Form1 : Form
    {
        private bool debug = false;

        // the abstraction for using winmm.dll. Functions in there are written as calls to the DLL
        private Mp3Player player;
        // class for handling the global hotkeys, their registration, and unregistering them when the program closes
        private GlobalHotkeys gh;
        private ToolTip toolTip;

        // These two dictionaries are used to represent the playlist. The key is the ID in the playlist, and using that key
        // you can get the relevant track object, containing such information such as file path, or duration
        private Dictionary<int, Track> memoryPlaylist = new Dictionary<int, Track>();
        private Playlist mainPlaylist = new Playlist();
        // a separate dictionary is used for shuffle functionality
        private Dictionary<int, Track> shufflePlaylist = new Dictionary<int, Track>();

        private int currentSongId;
        private bool shuffleEnabled = false;
        private int rewindCount = 0;
        string currentSeekBarPositionInString;

        // if the player is paused or not
        private bool isPlaying = false;
        private bool isMuted = false;

        // if the playlist should be automatically saved when closing the program
        bool isAutoSave = true;

        private int volume;
        private int oldVolume;
        // Setting name is the key, and the value of the setting is the value. Ex: <shuffle, true>
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        // variables for checking if the song has finished playing.
        private const int MM_MCINOTIFY = 0x03b9;
        private const int MCI_NOTIFY_SUCCESS = 0x01;
        private const int MCI_NOTIFY_SUPERSEDED = 0x02;
        private const int MCI_NOTIFY_ABORTED = 0x04;
        private const int MCI_NOTIFY_FAILURE = 0x08;


        /*
        **
        ** Constructor
        **
        **
        */
        public Form1()
        {
            InitializeComponent();
            player = new Mp3Player(this);
            gh = new GlobalHotkeys(this);

            // The initial value of where the song should start playing from is the beginning
            seekBar.Value = 0;
            // Select the first song in the playlist to play
            currentSongId = 0;
            currentSeekBarPositionInString = "0";





            SetUpListView();


            LoadPlaylist();
            LoadSettings();

            setUpIcons();
            setUpToolTips();
        }

        private void setUpIcons()
        {
            btnOpen.Image = Resources.open;
            btnPlay.Image = Resources.playNew;
            btnPause.Image = Resources.pause;
            btnStop.Image = Resources.stop;
            btnNext.Image = Resources.next;
            btnPrevious.Image = Resources.previous;
            btnRandom.Image = Resources.shuffle;
            pbxVolume.Image = Resources.volume;
        }

        private void setUpToolTips()
        {
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 0;
            toolTip.InitialDelay = 0;
            toolTip.ReshowDelay = 0;
            toolTip.ShowAlways = false;

            toolTip.SetToolTip(btnOpen, "Open a new song");
            toolTip.SetToolTip(btnPrevious, "Play previous song");
            toolTip.SetToolTip(btnPlay, "Start playback");
            toolTip.SetToolTip(btnPause, "Pause playback");
            toolTip.SetToolTip(btnStop, "Stop playback");
            toolTip.SetToolTip(btnNext, "Play next song");
            toolTip.SetToolTip(btnRandom, "Play a random song from the playlist");
            toolTip.SetToolTip(chkShuffle, "Toggle shuffle playback");
            toolTip.SetToolTip(chkIsAutoSave, "Automatically save playlist, and playback position when closing the player");
            toolTip.SetToolTip(btnSave, "Save a auto-generated playlist based on the current playlist");
            toolTip.SetToolTip(btnLoad, "Load a saved playlist. Can only load auto-generated playlist");


        }

        // Add headers to the playlist, and make sure the view is set to "Details"
        private void SetUpListView()
        {
            String[] playlistColumnNameList = new String[] { "ID", "Artist", "Title", "Length", "Date Added" };

            foreach (String columnName in playlistColumnNameList)
            {
                lvPLaylist.Columns.Add(columnName);
            }

            // Default width of each column
            lvPLaylist.View = View.Details;
            lvPLaylist.Columns[0].Width = 30;
            lvPLaylist.Columns[1].Width = 100;
            lvPLaylist.Columns[2].Width = 250;
            lvPLaylist.Columns[3].Width = 39;
            lvPLaylist.Columns[4].Width = 111;
        }

        // Add a song from a file to a playlist that is stored in nemory
        private int addToMainPlaylist(Track track)
        {
            PlaylistItem item = mainPlaylist.add(track);
            addToListView(item);
            return item.position;

        }

        // Add a song from the playlist that is in memory, to the listview, so it would be 
        // visible to the user
        private void addToListView(PlaylistItem item)
        {
            ListViewItem lvi = new ListViewItem(item.position.ToString());
            lvi.SubItems.Add(item.track.artist);
            lvi.SubItems.Add(item.track.title);
            lvi.SubItems.Add(item.track.length);
            lvi.SubItems.Add(item.track.dateAdded.ToString());

            lvPLaylist.Items.Add(lvi);

        }

        // Adds the previous song to the playlist of songs that were already played
        // If the current song is already the last thing played, it will not add to past playlist
        // Sets the new song with id [i] to be next song to be played
        private void setCurrentSong(int i)
        {
            Track currentTrack = getCurrentTrack();


            if (mainPlaylist.size > 0)
            unpaintItemAt(getListViewItemIndexById(currentSongId));
            currentSongId = i;
            paintAsCurrentSong(getListViewItemIndexById(currentSongId));
        }

        private void paintAsCurrentSong(int n)
        {
            if (lvPLaylist.Items[n] != null)
            {
                lvPLaylist.Items[n].BackColor = Color.Gray;
                lvPLaylist.Items[n].ForeColor = Color.Black;
            }

        }

        private void lvPLaylist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String currentSongIdString = lvPLaylist.SelectedItems[0].SubItems[0].Text;
            int currentSongId = int.Parse(currentSongIdString);
            setCurrentSong(currentSongId);
            OpenCurrentSong();
        }



        private void OpenCurrentSong()
        {
            if (mainPlaylist.size > 0)
            {
                Track currentTrack = getCurrentTrack();
                Open(currentTrack.filePath);
            }
            else
            {
                MessageBox.Show("Error", "Trying to play a song which has index greater than the number of items in playlist");
            }
        }

        private void Open(string path)
        {
            Console.WriteLine("Opening... {0}", System.IO.Path.GetFileNameWithoutExtension(path));
            player.Open(path);
            Play();
        }

        private void Play()
        {
            player.Play();
            updateCurrentSongInfo();
            seekerUpdate.Enabled = true;
            isPlaying = true;

        }

        private void updateCurrentSongInfo()
        {
            updateCurrentSongInformation();
            //updateCurrentSongLenght();
            updateSeekBar();
        }

        private void updateCurrentSongInformation()
        {
            Track currentTrack = getCurrentTrack();
            string prefix = "Currently playing: ";
            string artist = currentTrack.artist;
            string title = currentTrack.title;
            string text = String.Format("{0}{1} - {2}", prefix, artist, title);
            lblCurrentlyPlaying.Text = text;

            // delete updateCurrentSongLenght() if below works
            lblCurrentSongLength.Text = currentTrack.length;
        }

        private void updateCurrentSongLenght()
        {
            string prefix = "Length: ";
            long length = player.currentSongLenght;
            int totalSeconds = (int)(length / 1000);
            int totalMinutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            lblCurrentSongLength.Text = prefix + totalMinutes + ":" + remainingSeconds;
        }

        // Used to set the max value (milliseconds) of the seekbar. It ticks every second, to show
        // the progress of the song
        private void updateSeekBar()
        {
            long max = player.currentSongLenght;
            seekBar.Maximum = (int)max;
        }














        private Track getCurrentTrack()
        {
            return mainPlaylist.getTrackByPosition(currentSongId);
        }



        private void Pause()
        {
            if (isPlaying)
            {
                player.Pause();
                seekerUpdate.Enabled = false;
                isPlaying = false;
            }
            else
            {
                if (player.currentSongFilePath == null)
                {
                    OpenCurrentSong();
                }
                else
                {
                    Play();
                }

            }
        }

        private void Stop()
        {
            player.Stop();
            seekerUpdate.Enabled = false;
            seekBar.Value = 0;
            isPlaying = false;
        }











        private void clearPlaylist()
        {
            lvPLaylist.Items.Clear();
            mainPlaylist.clear();
        }

        private void playlist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }






        private void playlist_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string file in FileList)
            {

                if (System.IO.Directory.Exists(file))
                {
                    addFolder(file);
                }
                else
                {
                    if (extensionIsSupported(file))
                    {
                        addToMainPlaylist(new Track(file));
                    }

                }
            }
        }

        private void addFolder(string folderPath)
        {
            foreach (string file in System.IO.Directory.GetFiles(folderPath))
            {
                if (extensionIsSupported(file))
                {
                    addToMainPlaylist(new Track(file));
                }
            }
            foreach (string dir in System.IO.Directory.GetDirectories(folderPath))
            {
                addFolder(dir);
            }
        }






        private void updateTotalSongsLeft(int n)
        {
            string prefix = "Total songs left: ";
            lblTotalSongs.Text = prefix + n;
        }

        private bool extensionIsSupported(string songPath)
        {
            string extension = ".mp3";
            bool result = songPath.LastIndexOf(extension) > (songPath.Length - 5);
            return result;
        }


        protected override void WndProc(ref Message m)
        {

            if (m.Msg == MM_MCINOTIFY)
            {
                switch (m.WParam.ToInt32())
                {
                    case MCI_NOTIFY_SUCCESS:
                        FinishedPlaying();
                        break;
                    case MCI_NOTIFY_SUPERSEDED:
                        // superseded handling
                        break;
                    case MCI_NOTIFY_ABORTED:
                        // abort handling
                        break;
                    case MCI_NOTIFY_FAILURE:
                        // failure! handling
                        break;
                    default:
                        // haha
                        break;
                }
            }
            else if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                if (id == 0)
                {
                    Pause();
                }
                if (id == 1)
                {
                    playPrevious();
                }
                if (id == 2)
                {
                    playNext();
                }
            }
            base.WndProc(ref m);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlgOpen = new OpenFileDialog())
            {
                dlgOpen.Filter = "Mp3 File|*.mp3";
                dlgOpen.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dlgOpen.FileName;
                    if (extensionIsSupported(path))
                    {
                        clearPlaylist();
                        int songId = addToMainPlaylist(new Track(path));
                        setCurrentSong(songId);
                        OpenCurrentSong();

                    }
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            {
                if (memoryPlaylist.Count > 0)
                {
                    if (player.currentSongFilePath == null)
                    {
                        OpenCurrentSong();
                        return;
                    }
                    Play();
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            {
                Stop();
            }
        }



        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void seekBar_Scroll(object sender, EventArgs e)
        {
            int pos = seekBar.Value;
            player.Seek(pos);
            player.Play();
        }

        private void seekerUpdate_Tick(object sender, EventArgs e)
        {
            string prefix = "Position: ";
            long position = player.Position();
            if (position < 0)
            {
                position = 0;
            }
            seekBar.Value = (int)position;

            int totalSeconds = (int)(position / 1000);
            int totalMinutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;
            currentSeekBarPositionInString = prefix + totalMinutes + "m " + remainingSeconds + "s";
            toolTip.SetToolTip(seekBar, currentSeekBarPositionInString);

        }

        private void seekBar_MouseDown(object sender, MouseEventArgs e)
        {
            float w = seekBar.Size.Width;
            float mx = e.X;
            float p = mx / w;
            int l = (int)player.currentSongLenght;
            float newPos = l * p;

            Seek((int)newPos);


        }

        private void Seek(int n)
        {
            if (player.currentSongFilePath != null)
            {
                seekBar.Value = n;
                player.Seek(n);
                player.Play();
            }
        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            playPrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            playNext();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            PlayRandom();
        }

        private void FinishedPlaying()
        {
            playNext();
        }

        private void playPrevious()
        {
            if (shuffleEnabled)
            {
                
            }
            else
            {
                if(currentSongId > 0)
                {

                }
            }
        }



        private void playNext()
        {
            if (shuffleEnabled)
            {
                PlayRandom();
            }
            else
            {
                if (currentSongId == memoryPlaylist.Count - 1)
                {
                    setCurrentSong(0);
                }
                else
                {
                    setCurrentSong(currentSongId + 1);
                }
                OpenCurrentSong();

            }
        }

        private void PlayRandom()
        {
            Random random = new Random();
            int randomId = random.Next(0, shufflePlaylist.Count);
            shufflePlaylist.Remove(randomId);
            updateShuffleSongsRemaining();
            setCurrentSong(randomId);
            OpenCurrentSong();
            //appendRandomSongToFile(randomId);
        }

        private void updateShuffleSongsRemaining()
        {
            string prefix = "Shuffle songs left: ";
            int n = shufflePlaylist.Count;
            lblShuffleSongsLeft.Text = prefix + n;
            if (n < 2)
            {
                Console.WriteLine("Playlist count is" + n);
            }
        }



        //private void SavePlaylist()
        //{
        //    string savePath = @".\Settings\playlist.txt";
        //    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(savePath));

        //    System.IO.File.Delete(savePath);
        //    string[] lines = memoryPlaylist.Select(kvp => kvp.Key + "=" + kvp.Value.filePath).ToArray();
        //    System.IO.File.WriteAllLines(savePath, lines);
        //}

        private void chkShuffle_CheckedChanged(object sender, EventArgs e)
        {
            setShuffleStatus(chkShuffle.Checked);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePlaylist();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadPlaylist();
        }

        private void SavePlaylist()
        {
            string savePath = @".\Settings\playlist.txt";
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(savePath));

            System.IO.File.Delete(savePath);
            string[] lines = memoryPlaylist.Select(kvp => kvp.Key + "=" + kvp.Value.filePath).ToArray();
            System.IO.File.WriteAllLines(savePath, lines);

            int playlistSize = mainPlaylist.size;
            String[] savedPlaylist = new String[playlistSize];
            for (int i = 0; i < playlistSize; i++)
            {
                savedPlaylist[i] = mainPlaylist.getTrackByPosition(i).filePath;
            }
            System.IO.File.WriteAllLines(savePath, savedPlaylist);

        }

        private void LoadPlaylist()
        {
            string savePath = @".\Settings\playlist.txt";
            if (System.IO.File.Exists(savePath))
            {

                string[] lines = System.IO.File.ReadAllLines(savePath);


                clearPlaylist();

                foreach (string line in lines)
                {
                    addToMainPlaylist(new Track(line));
                }

            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gh.unregister();
            SaveSettings();
        }

        private void SaveSettings()
        {
            settings.Clear();
            string savePath = @".\Settings\settings.txt";
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(savePath));
            settings.Add("currentSongId", currentSongId.ToString());
            string currentSongPos = player.Position().ToString();
            settings.Add("currentSongPos", currentSongPos);
            settings.Add("tShuffle", shuffleEnabled.ToString());
            settings.Add("volume", volume.ToString());
            settings.Add("isAutoSave", isAutoSave.ToString());

            System.IO.File.Delete(savePath);
            string[] lines = settings.Select(kvp => kvp.Key + "=" + kvp.Value).ToArray();
            System.IO.File.WriteAllLines(savePath, lines);

            if (isAutoSave)
            {
                SavePlaylist();
            }
        }

        private void LoadSettings()
        {

            string savePath = @".\Settings\settings.txt";
            if (System.IO.File.Exists(savePath))
            {
                string[] lines = System.IO.File.ReadAllLines(savePath);
                settings = lines.Select(l => l.Split('=')).ToDictionary(a => a[0], a => a[1]);

                int songId = int.Parse(settings["currentSongId"]);
                int songPos = int.Parse(settings["currentSongPos"]);
                if (songPos < 0)
                {
                    songPos = 0;
                }
                bool tempShuffle = bool.Parse(settings["tShuffle"]);
                setVolume(int.Parse(settings["volume"]));
                bool tempIsAutoSave = bool.Parse(settings["isAutoSave"]);


                setCurrentSong(songId);
                OpenCurrentSong();
                seekBar.Maximum = (int)player.currentSongLenght;
                Seek(songPos);
                setShuffleStatus(tempShuffle);
                setIsAutoSave(tempIsAutoSave);
            }
            else
            {
                LoadDefaultSettings();
            }
        }

        private void LoadDefaultSettings()
        {
            volume = 1000;
            isAutoSave = true;

        }

        private void setShuffleStatus(bool b)
        {
            shuffleEnabled = b;
            chkShuffle.Checked = b;
            if (shufflePlaylist.Count < 1)
            {
                shufflePlaylist = new Dictionary<int, Track>(memoryPlaylist);
            }
        }

        private void tkbVolume_Scroll(object sender, EventArgs e)
        {
            setVolume(tkbVolume.Value);
        }



        private void tkbVolume_MouseDown(object sender, MouseEventArgs e)
        {
            float w = tkbVolume.Size.Width;
            float mx = e.X;
            float p = mx / w; ;
            float newPos = tkbVolume.Maximum * p;

            setVolume((int)newPos);
        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            isAutoSave = chkIsAutoSave.Checked;
        }

        private void setIsAutoSave(bool state)
        {
            isAutoSave = state;
            chkIsAutoSave.Checked = state;
        }




        ListViewItem mouseDownItem;
        int newMovedTrackIndex;
        bool isItemHeld;


        private void lvPLaylist_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownItem = lvPLaylist.GetItemAt(e.X, e.Y);
            if (mouseDownItem != null && lvPLaylist.SelectedItems.Count > 0 && lvPLaylist.SelectedItems[0].Equals(mouseDownItem))
            {
                isItemHeld = true;
                newMovedTrackIndex = mouseDownItem.Index;

            }

        }

        private void lvPLaylist_MouseMove1(object sender, MouseEventArgs e) { }


        private void lvPLaylist_MouseMove(object sender, MouseEventArgs e)
        {

            if (isItemHeld)
            {
                ListViewItem itemAtMousePosition = lvPLaylist.GetItemAt(e.X, e.Y);
                if (itemAtMousePosition != null && itemAtMousePosition.Index != newMovedTrackIndex)
                {
                    setNewIndex(itemAtMousePosition.Index);
                }
            }
        }


        private void setNewIndex(int n)
        {
            unpaintItemAt(newMovedTrackIndex);
            paintNewLocation(n);
            newMovedTrackIndex = n;
        }

        private void paintNewLocation(int n)
        {
            lvPLaylist.Items[n].BackColor = Color.Black;
            lvPLaylist.Items[n].ForeColor = Color.White;
        }

        private void unpaintItemAt(int n)
        {
            if(n < 0)
            {
                Console.WriteLine("Tried to unpaint item at position {0}, only non-negative positions are allowed. Ignored", n);
                return;
            }
            lvPLaylist.Items[n].BackColor = Color.White;
            lvPLaylist.Items[n].ForeColor = Color.Black;
        }

        private void lvPLaylist_MouseUp1(object sender, MouseEventArgs e) { }

        private void lvPLaylist_MouseUp(object sender, MouseEventArgs e)
        {
            if (isItemHeld)
            {
                if (!newMovedTrackIndex.Equals(lvPLaylist.SelectedItems[0].Index))
                {
                    unpaintItemAt(newMovedTrackIndex);
                }
                lvPLaylist.Items.RemoveAt(mouseDownItem.Index);
                lvPLaylist.Items.Insert(newMovedTrackIndex, mouseDownItem);
                isItemHeld = false;
            }

        }

        private void pbxVolume_MouseDown(object sender, MouseEventArgs e)
        {
            if (isMuted)
            {
                unMute();
            }
            else
            {

                mute();
            }
        }

        private void mute()
        {
            isMuted = true;
            oldVolume = volume;
            setVolume(0);

            pbxVolume.Image = Resources.mute;

        }

        private void unMute()
        {
            isMuted = false;
            setVolume(oldVolume);
            pbxVolume.Image = Resources.volume;

        }

        private void setVolume(int newVolume)
        {
            if (newVolume < 0 && newVolume > 1000)
            {
                Console.WriteLine("Volume change to " + newVolume + " was requested. It exceeds limits. Ignored");
                return;
            }

            if (isMuted)
            {
                if (newVolume > 0)
                {
                    unMute();
                }
            }
            else if (newVolume < 1)
            {
                mute();
            }

            volume = newVolume;
            tkbVolume.Value = newVolume;
            player.SetVolume(newVolume);


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete && lvPLaylist.SelectedItems.Count > 0)
            {

                string stringId = lvPLaylist.SelectedItems[0].SubItems[0].Text;
                int id = int.Parse(stringId);
                mainPlaylist.remove(id);
                lvPLaylist.Items.Remove(lvPLaylist.SelectedItems[0]);

            }
        }

        private int getListViewItemIndexById(int n)
        {
            string id = n.ToString();
            foreach (ListViewItem item in lvPLaylist.Items)
            {
                if (item.SubItems[0].Text == id)
                {
                    return item.Index;
                }
            }
            Console.WriteLine("Tried to get ListViewItem with ID of {0}, none found. Returning -1", n);
            return -1;
        }


    }




}
