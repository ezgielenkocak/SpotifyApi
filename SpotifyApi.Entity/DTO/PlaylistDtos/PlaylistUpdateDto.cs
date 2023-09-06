using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistDtos
{
    public class PlaylistUpdateDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public string? Type { get; set; }
    }
}
