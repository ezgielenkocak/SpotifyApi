using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.PlaylistDtos;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistController : ControllerBase
    {
        private IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpPost("addplaylist")]
        public IActionResult AddPlaylist(PlaylistCreateDto dto)
        {
            var result = _playlistService.Add(dto);
            return Ok(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetAll()
        {
            var result = _playlistService.GetAll();
            return Ok(result);
        }

     

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _playlistService.Delete(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdatePlayList(PlaylistUpdateDto dto)
        {
            var result = _playlistService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetByIdPlayList(int id)
        {
            var result = _playlistService.GetById(id);
            return Ok(result);
        }



    }
}
