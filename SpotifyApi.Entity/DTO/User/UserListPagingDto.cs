using SpotifyApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Entity.DTO.User
{
    public class UserListPagingDto:IDto
    {
        public List<UserListDto> Users { get; set; }
        public PagingDto Page { get; set; }
    }
}
