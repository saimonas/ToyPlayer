using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevoPlayer
{
    class Playlist : IEnumerator, IEnumerable
    {
        public bool isEmpty { get; private set; }

        // needed for implementing IEnumerable
        private PlaylistItem currentItem;
        // Needed to implement IEnumerable iterface
        private int IEnumerablePosition = -1;

        private List<PlaylistItem> memoryPlaylist;
        public ListView lvPlaylist { get; set; }


        public int size { get { return memoryPlaylist.Count;} }

        public Playlist(ListView listView)
        {
            this.memoryPlaylist = new List<PlaylistItem>();
            this.isEmpty = true;
            this.lvPlaylist = listView;
            SetUpListView();
            
        }

        private void SetUpListView()
        {

            String[] playlistColumnNameList = new String[] { "ID", "Artist", "Title", "Length", "Date Added" };

            foreach (String columnName in playlistColumnNameList)
            {
                lvPlaylist.Columns.Add(columnName);
            }

            // Default width of each column
            lvPlaylist.View = View.Details;
            lvPlaylist.Columns[0].Width = 30;
            lvPlaylist.Columns[1].Width = 100;
            lvPlaylist.Columns[2].Width = 250;
            lvPlaylist.Columns[3].Width = 39;
            lvPlaylist.Columns[4].Width = 111;
        }



        public PlaylistItem add(Track track)
        {
            /* Creates a new object to use in the playlist, from the track information and gives it the position
            ** which equals to the size of the playlist i.e. last */
            PlaylistItem item = new PlaylistItem(track, this.size);

            ListViewItem lvi = new ListViewItem(item.position.ToString());
            lvi.SubItems.Add(track.artist);
            lvi.SubItems.Add(track.title);
            lvi.SubItems.Add(track.length);
            lvi.SubItems.Add(track.dateAdded.ToString());

            lvPlaylist.Items.Add(lvi);


            this.memoryPlaylist.Add(item);
            this.isEmpty = false;
            return item;

        }

        public void removeAt(int positionInPlaylist)
        {
            if(positionInPlaylist > this.size)
            {
                Console.WriteLine("Error: Tried to remove item from playlist at position [{0}], playlist size is [{1}]",
                    positionInPlaylist, this.size);
                return;
            }
            memoryPlaylist.RemoveAt(positionInPlaylist);
            lvPlaylist.Items.RemoveAt(positionInPlaylist);
            //decrementTrackPositionBelow(positionInPlaylist);     

            if(this.size < 1)
            {
                this.isEmpty = true;
            }       
        }

        private void decrementTrackPositionBelow(int n)
        {
            foreach (PlaylistItem item in memoryPlaylist)
            {
                if(item.position > n)
                {
                    item.position += -1;
                }
            }
        }

        public void move(int fromPosition, int toPosition)
        {
            if( fromPosition < 0 || fromPosition > this.size || toPosition < 0 || toPosition > this.size)
            {
                Console.WriteLine("Tried to move item from position {0} to position {1}. Min value allowed {2}, max {3}. Ignored.", 
                    fromPosition, toPosition, 0, this.size);
                return;
            }

            PlaylistItem fromMemoryItem = memoryPlaylist.ElementAt(fromPosition);
            ListViewItem fromLvItem = lvPlaylist.Items[fromPosition];

            memoryPlaylist.RemoveAt(fromPosition);
            lvPlaylist.Items.RemoveAt(fromPosition);

            memoryPlaylist.Insert(toPosition, fromMemoryItem);
            lvPlaylist.Items.Insert(toPosition, fromLvItem);
        }

        public Track getTrackByPosition(int position)
        {
            if( (this.size - 1) < position)
            {
                Console.WriteLine("Error: Tried to get track number {0} from playlist. Onlye {1} items in playlist.", 
                    position, size);
                return null;
            }else if (position < 0)
            {
                Console.WriteLine("Error: Tried to get track number {0}. Track number cannot be negative.",
                    position);
            }
            return memoryPlaylist.ElementAt(position).track;
        }

        public void clear()
        {
            memoryPlaylist.Clear();

            foreach (ListViewItem item in lvPlaylist.Items) 
            {
                lvPlaylist.Items.Remove(item);
            }

            
        }

        public PlaylistItem getLastItem()
        {
            if(memoryPlaylist.Count < 1)
            {
                Console.WriteLine("Error: Tried to retrieve last item from playlist, but the playlist has no items."
                    +" Returning null");
                return null;
            }
            return memoryPlaylist.Last();
        }


        

        bool IEnumerator.MoveNext()
        {
            IEnumerablePosition++;
            return (IEnumerablePosition < this.size);
        }

        void IEnumerator.Reset()
        {
            IEnumerablePosition = -1;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        object IEnumerator.Current { get { return memoryPlaylist.ElementAt(IEnumerablePosition); } }
    }
}
