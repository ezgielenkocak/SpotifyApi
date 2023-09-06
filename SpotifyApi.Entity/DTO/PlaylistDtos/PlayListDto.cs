using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistDtos
{
    public class PlayListDto
    {
        public int Id { get; set; }
        public string PlaylistId { get; set; }

        public string TrackName { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
