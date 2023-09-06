using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.SongPoolDto
{
    public class SongPoolPagingDto : IDto
    {
        public List<SongPoolListDto> Tracks { get; set; }
        public PagingDto Page { get; set; }
    }
}
