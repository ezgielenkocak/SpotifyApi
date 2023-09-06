using Azure.Core;
using Newtonsoft.Json;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO;
using SpotifyApi.Entity.DTO.SpotifApiDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.SongPoolDto;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class SongPoolManager : ISongService
    {
        //public IDataResult<Song> GetLastSong(string token)
        //{
        //    var url = "$https://api.spotify.com/v1/playlists/37i9dQZF1EUMDoJuT8yJsl/tracks";
        //    var get = await ConnectApi<Song>(url, token);
        //    var albums = await ConnectApi<Song>(url, token);
        //    return new SuccessDataResult<Song>(albums.Data, "Ok", Messages.success);
        //}


        public async Task<IDataResult<SongPoolAlbumDto>> GetAlbums(string token)
        {
            try
            {
                var url = $"https://api.spotify.com/v1/browse/new-releases?";
                var albums = await ConnectApi<SongPoolAlbumDto>(url, token);
                if (albums.Data == null)
                {
                    return new ErrorDataResult<SongPoolAlbumDto>(null, albums.Message, albums.MessageCode);
                }
                return new SuccessDataResult<SongPoolAlbumDto>(albums.Data, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SongPoolAlbumDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public async Task<IDataResult<SongPoolPagingDto>> GetSongs(string token, int pageSize, int pageNumber)
        {
            try
            {
              
              
                var albums = await GetAlbums(token);
                if(albums.Data == null)
                {
                    return new ErrorDataResult<SongPoolPagingDto>(null, albums.Message, albums.MessageCode);
                }
                var ids = albums.Data.Albums.Items.Select(a => a.Id);
                var url = $""; 
                var trackPool = new List<SongPoolListDto>();
                foreach (var item in ids)
                {
                    url = $"https://api.spotify.com/v1/albums/{item}/tracks?market=TR&limit=2";
                    trackPool.AddRange(ConnectApi<SongPoolDto>(url, token).Result.Data.Items);
                }

                pageNumber = pageNumber <= 1 ? 0 : pageNumber - 1;
                pageSize = pageSize < 1 ? 1 : pageSize;

                var skip = pageSize * pageNumber;
                var totalCount = trackPool.Count;
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                trackPool = trackPool.Skip(skip).Take(pageSize).ToList();

                var resultDto = new SongPoolPagingDto
                {
                    Tracks = trackPool,
                    Page = new PagingDto
                    {
                        Page = pageNumber + 1,
                        Size = pageSize,
                        TotalCount = totalCount,
                        TotalPages = totalPages
                    }
                };

                return new SuccessDataResult<SongPoolPagingDto>(resultDto, "Ok", Messages.success);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SongPoolPagingDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public async Task<IDataResult<T>> ConnectApi<T>(string url, string token)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Headers =
                   {
                    { "Accept", "application/json" },
                    { "Authorization", $"Bearer {token}" }
                   },
                };

                   var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var query = await response.Content.ReadAsStringAsync();
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    var songs = JsonConvert.DeserializeObject<T>(query);
                    return new SuccessDataResult<T>(songs, "Ok", Messages.success);
                
            }
            catch (Exception e)
            {
                return new ErrorDataResult<T>(default, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<Song> GetList(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,

                RequestUri = new Uri("https://api.spotify.com/v1/playlists/37i9dQZF1EUMDoJuT8yJsl/tracks"),
                Headers =
                {

                    {"Accept",  "application/json"},
                    {"Authorization", $"Bearer {token}" }
                },
            };
            var response = client.SendAsync(request).Result;
            var test = response.Content.ReadAsStringAsync().Result;
            Song playlist = JsonConvert.DeserializeObject<Song>(test);

            return new SuccessDataResult<Song>(new Song
            {

                items = playlist.items,

                track = playlist.track,

                album = playlist.album,


            });
        }
    }
}
