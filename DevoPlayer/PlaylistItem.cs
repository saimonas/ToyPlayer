using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevoPlayer
{
    class PlaylistItem
    {
        public Track track { get; }
        public int position { get; set; }
        

        public PlaylistItem(Track track, int position)
        {
            this.track = track;
            this.position = position;
            
        }

    }
}
