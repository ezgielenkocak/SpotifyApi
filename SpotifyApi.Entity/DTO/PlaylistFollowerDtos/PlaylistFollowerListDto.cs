﻿using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.PlaylistFollowerDtos
{
    public class PlaylistFollowerListDto:IDto
    {
        public int Id { get; set; }
        public string PlaylistId { get; set; }
        public int FollowerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
