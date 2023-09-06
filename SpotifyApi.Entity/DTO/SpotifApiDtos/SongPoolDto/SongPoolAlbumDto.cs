using SpotifyApi.Core.Entities;
using SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos
{
    public class SongPoolAlbumDto : IDto
    {
        public SongPoolAlbumListDto Albums { get; set; }
        public List<SongPoolDetailDto> Items { get; set; }
    }
    public class SongPoolAlbumListDto
    {
        public List<SongPoolDetailDto> Items { get; set; }
       
    }
    public class SongPoolDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    
        public int Total_Tracks { get; set; }
        public string Type { get; set; }
        
    }
}
