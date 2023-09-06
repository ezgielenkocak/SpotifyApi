using SpotifyApi.Core.Entities;
using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.SpotifApiDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.SongPoolDto;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface ISongService
    {
        public IDataResult<Song> GetList(string token);

        Task<IDataResult<SongPoolAlbumDto>> GetAlbums(string token);
        //Task<IDataResult<Song>> GetSong(string token);
        Task<IDataResult<SongPoolPagingDto>> GetSongs(string token, int pageSize, int pageNumber);
        Task<IDataResult<T>> ConnectApi<T>(string url, string token);
    }
}
