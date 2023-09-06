using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistDtos
{
    public class PlaylistCreateDto:IDto
    {
        public string Token { get; set; }
        public string PlaylistId { get; set; }
        public string TrackName { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
    }
}
