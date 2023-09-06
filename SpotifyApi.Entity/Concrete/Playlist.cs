using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.Concrete
{
    public class Playlist:IEntity
    {
        public int Id { get; set; }
        public string PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public string TrackName { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        //public string? Type { get; set; }



    }
}
