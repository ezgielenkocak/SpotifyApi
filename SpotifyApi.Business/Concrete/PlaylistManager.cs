using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.EntityFramework;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class PlaylistManager : IPlaylistService
    {
        private IPlaylistDal _playlistDal;
        private readonly ISongService _trackPoolService;
        private readonly IUserService _userService;

        public PlaylistManager(IPlaylistDal playlistDal, ISongService trackPoolService, IUserService userService)
        {
            _playlistDal = playlistDal;
            _trackPoolService = trackPoolService;
            _userService = userService;
        }

        public IDataResult<bool> Add(PlaylistCreateDto playlistCreateDto)
        {
            try
            {
                if (playlistCreateDto != null)
                {
                    if (String.IsNullOrEmpty(playlistCreateDto.PlaylistId))
                    {
                        return new ErrorDataResult<bool>(false, "Id can not be null", Messages.err_null);
                    }
                    var url = $"https://api.spotify.com/v1/playlists/{playlistCreateDto.PlaylistId}?market=TR";
                    var data = _trackPoolService.ConnectApi<SongPoolDetailDto>(url, playlistCreateDto.Token).Result;
                    if (data.Success)
                    {
                        var playlist = new Playlist
                        {
                            PlaylistId = playlistCreateDto.PlaylistId,
                            PlaylistName = playlistCreateDto.TrackName,
                            UserId = playlistCreateDto.UserId,
                            TrackName = data.Data.Name,

                            CreatedDate = DateTime.Now,
                            Status = true,
                        };
                        _playlistDal.Add(playlist);
                        return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                    };
                    return new ErrorDataResult<bool>(false, "not found!", Messages.err_null);
                }

                else
                {
                    var playlist = new Playlist()
                    {
                        PlaylistId = playlistCreateDto.PlaylistId,
                        UserId = playlistCreateDto.UserId,
                        TrackName = playlistCreateDto.TrackName,

                        CreatedDate = DateTime.Now,
                        Status = true,
                    };
                    _playlistDal.Add(playlist);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }

                return new ErrorDataResult<bool>(false, "Given dto is null", Messages.err_null);
            }

            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.err_null);

            }
        }
    

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var playList = _playlistDal.Get(x => x.Id == id);
                if (playList != null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                playList.Status = false;
                _playlistDal.Update(playList);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);


            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, "", e.Message);

            }
        }

        public IDataResult<PlayListDto> GetById(int id)
        {
            try
            {
                var playlist = _playlistDal.Get(x => x.Id == id);
                if (playlist == null)
                {
                    return new ErrorDataResult<PlayListDto>(null, "", Messages.unknown_err);
                }
                return new SuccessDataResult<PlayListDto>(new PlayListDto
                {
                    Id = playlist.Id,
                    PlaylistId=playlist.PlaylistId,
                    TrackName = playlist.TrackName,
                    UserName = _userService.Get(u => u.Id == playlist.UserId).Data != null ? _userService.Get(u => u.Id == playlist.UserId).Data.Username : "",
                    CreatedDate = playlist.CreatedDate,
                    Status = playlist.Status,
                });
            }
            catch (Exception e)
            {
                return new ErrorDataResult<PlayListDto>(null, "", e.Message);
            }
        }

        public IDataResult<List<PlayListDto>> GetAll()
        {
            try
            {
                var getPlaylists = _playlistDal.GetList();
                if (getPlaylists == null)
                {
                    return new ErrorDataResult<List<PlayListDto>>(null, "", Messages.err_null);
                }
                var playlistDto = new List<PlayListDto>();
                foreach (var playlist in getPlaylists)
                {
                    playlistDto.Add(new PlayListDto
                    {
                        Id = playlist.Id,
                        PlaylistId= playlist.PlaylistId,
                        TrackName = playlist.TrackName,
                        UserName =  _userService.Get(u => u.Id == playlist.UserId).Data.Username ,
                        CreatedDate = playlist.CreatedDate,
                        Status = playlist.Status,
                    });
                }
                return new SuccessDataResult<List<PlayListDto>>(playlistDto, "", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<PlayListDto>>(null, "", e.Message);
            }
        }

     

        public IDataResult<bool> Update(PlaylistUpdateDto playlistUpdateDto)
        {
            try
            {
                var updatePlaylist = _playlistDal.Get(x => x.PlaylistId == playlistUpdateDto.Id);
                if (updatePlaylist == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                updatePlaylist.UserId = playlistUpdateDto.UserId;
                updatePlaylist.TrackName = playlistUpdateDto.Name;
                updatePlaylist.Status = playlistUpdateDto.Status;
             

                _playlistDal.Update(updatePlaylist);
                return new SuccessDataResult<bool>(true, "", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, "", e.Message);
            }
        }

     
    }
}
