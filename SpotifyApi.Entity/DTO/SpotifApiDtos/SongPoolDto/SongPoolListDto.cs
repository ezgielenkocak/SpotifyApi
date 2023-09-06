using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.SpotifApiDtos.SongPoolDto
{
    public class SongPoolListDto:IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int Track_Number { get; set; }
        public string Type { get; set; }
    }
}
