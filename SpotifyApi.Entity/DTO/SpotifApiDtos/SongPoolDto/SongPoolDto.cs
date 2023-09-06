using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.SongPoolDto
{
    public class SongPoolDto : IDto
    {
        public List<SongPoolListDto> Items { get; set; }
        public int Total { get; set; }
    }
}
