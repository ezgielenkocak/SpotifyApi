using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.PlaylistFollowerDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistFollowerController : ControllerBase
    {
        private IPlaylistFollowerService _playlistFollowerService;

        public PlaylistFollowerController(IPlaylistFollowerService playlistFollowerService)
        {
            _playlistFollowerService = playlistFollowerService;
        }

        [HttpPost("addplaylist")]
        public IActionResult AddPlaylist(PlaylistFollowerCreateDto dto)
        {
            var result = _playlistFollowerService.Add(dto);
            return Ok(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetAll()
        {
            var result = _playlistFollowerService.GetAll();
            return Ok(result);
        }

        [HttpPost("removeplaylist")]
        public IActionResult Delete(int id)
        {
            var result = _playlistFollowerService.Delete(id);
            return Ok(result);
        }

        [HttpPost("updateplaylist")]
        public IActionResult UpdatePlayList(PlaylistFollowerUpdateDto dto)
        {
            var result = _playlistFollowerService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getplaylistbyid")]
        public IActionResult GetByIdPlayList(int id)
        {
            var result = _playlistFollowerService.GetById(id);
            return Ok(result);
        }
    }
}
