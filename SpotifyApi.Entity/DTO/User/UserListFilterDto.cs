using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.User
{
    public class UserListFilterDto:IDto
    {

        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Search { get; set; }
        public PagingFilterDto PagingFilterDto { get; set; }
    }
}
