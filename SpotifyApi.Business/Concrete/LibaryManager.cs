using Newtonsoft.Json.Linq;
using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.DataAccess.Concrete.EntityFramework;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.LibraryDtos;
using SpotifyApi.Entity.DTO.PlaylistDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.AlbumDtos;
using SpotifyApi.Entity.DTO.SpotifApiDtos.TrackPoolAlbumDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class LibaryManager : ILibaryService
    {
        private readonly IPlaylistDal _playlistDal;
        private readonly ISongService _trackPoolService;
        private readonly ILibraryDal _libraryDal;

        public LibaryManager(ILibraryDal libraryDal, IPlaylistDal playlistDal, ISongService trackPoolService)
        {
            _libraryDal = libraryDal;
            _playlistDal = playlistDal;
            _trackPoolService = trackPoolService;
        }

        public IDataResult<bool> Add(LibaryCreateDto libraryCreateDto)
        {
            try
            {
                if (libraryCreateDto != null)
                {
                    var library = new Library()
                    {
                        Type = libraryCreateDto.Type,
                        TypeId = libraryCreateDto.TypeId,
                        UserId = libraryCreateDto.UserId,
                        CreatedDate = DateTime.Now
                    };
                    _libraryDal.Add(library);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "", Messages.err_null);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var library = _libraryDal.Get(x => x.Id == id);
                if (library == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                _libraryDal.Delete(library);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<List<LibaryListDto>> GetAll(string token)
        {
            try
            {
                var libraryList = _libraryDal.GetList();
                if (libraryList == null )
                {
                    return new ErrorDataResult<List<LibaryListDto>>(null, "", Messages.err_null);
                }
                var libaryListDto = new List<LibaryListDto>();
                foreach (var libary in libraryList)
                {
                  
                    libaryListDto.Add(new LibaryListDto
                    {
                        Id = libary.Id,
                        UserId=libary.UserId,
                        Type = libary.Type,
                        
                        TypeId = libary.TypeId,
                        
                        CreatedDate = libary.CreatedDate
                    });
                }
                return new SuccessDataResult<List<LibaryListDto>>(libaryListDto, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<LibaryListDto>>(null, e.Message, Messages.unknown_err);
            }
        }

        public IDataResult<LibaryListDto> GetById(int id, string token)
        {
            try
            {
                var libraryList = _libraryDal.Get(x => x.Id == id);
                if (libraryList == null)
                {
                    return new ErrorDataResult<LibaryListDto>(null, "", Messages.unknown_err);
                }
             
                return new SuccessDataResult<LibaryListDto>(new LibaryListDto
                {
                    Id = libraryList.Id,
                    Type = libraryList.Type,
                    TypeId = libraryList.TypeId,
                    CreatedDate = libraryList.CreatedDate
                });
            }
            catch (Exception e)
            {
                return new ErrorDataResult<LibaryListDto>(null, e.Message, Messages.unknown_err);
            }
        }

      
        public IDataResult<bool> Update(LibaryUpdateDto libraryUpdateDto)
        {
            try
            {
                var library = _libraryDal.Get(x => x.Id == libraryUpdateDto.Id);
                if (library == null)
                {
                    return new ErrorDataResult<bool>(false, "", Messages.err_null);
                }
                library.UserId = libraryUpdateDto.UserId;
                library.Type = libraryUpdateDto.Type;
                library.TypeId = libraryUpdateDto.TypeId;

                _libraryDal.Update(library);
                return new SuccessDataResult<bool>(true, "Ok", Messages.success);
            }
            catch (Exception e)
            {
                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
            }
        }


    }
}
