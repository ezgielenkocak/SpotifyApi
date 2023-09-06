using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos
{
    public class SongPoolAlbumListDto : IDto
    {
        public string Href { get; set; }
        public int Id { get; set; }
        public int Total { get; set; }
    }
}
