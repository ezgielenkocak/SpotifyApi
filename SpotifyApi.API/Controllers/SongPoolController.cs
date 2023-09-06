using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Entity.DTO.SpotifApiDtos;
using System.Collections.Generic;

namespace SpotifyApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongPoolController : ControllerBase
    {
        private readonly ISongService _trackPoolService;

        public SongPoolController(ISongService trackPoolService)
        {
            _trackPoolService = trackPoolService;
        }
        [HttpGet("getsong")]
        public IActionResult GetList(string token)
            {
            var result=_trackPoolService.GetList(token);
            return Ok(result);  
        }
        [HttpGet("getsongwithpaging")]
        public async Task<IActionResult> GetTracks(string token, int pageSize, int pageNumber)
        {
            var result = await _trackPoolService.GetSongs(token, pageSize, pageNumber);
            return Ok(result);
        }
        [HttpGet("getalbums")]
        public async Task<IActionResult> GetAlbums(string token)
        {
            var result = await _trackPoolService.GetAlbums(token);
            return Ok(result);
        }
        //[HttpGet("getsong")] 
        //public async Task<ActionResult> GetSong(string token)
        //{
        //    var result=await _trackPoolService.SarkiGetir(token);
        //    return Ok(result);
        //}
        
   

    
    }
}
