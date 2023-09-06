using SpotifyApi.Core.Result;
using SpotifyApi.Entity.Concrete;
using SpotifyApi.Entity.DTO.LibraryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Abstract
{
    public interface ILibaryService
    {
        IDataResult<bool> Add(LibaryCreateDto libraryCreateDto);
        IDataResult<bool> Delete(int id);
        IDataResult<bool> Update(LibaryUpdateDto libraryUpdateDto);
        IDataResult<List<LibaryListDto>> GetAll(string token);
        IDataResult<LibaryListDto> GetById(int id, string token);

    }
}
