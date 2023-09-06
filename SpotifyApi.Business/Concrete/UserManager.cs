using SpotifyApi.Business.Abstract;
using SpotifyApi.Business.Constants;
using SpotifyApi.Core.Entities.Concrete;
using SpotifyApi.Core.Result;
using SpotifyApi.DataAccess.Abstract;
using SpotifyApi.Entity.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private ILibaryService _libraryService;

        public UserManager(IUserDal userDal, ILibaryService libraryService)
        {
            _userDal = userDal;
            _libraryService = libraryService;
        }

        public IDataResult<bool> Create(User user)
        {
            try
            {
                if (user != null)
                {
                    _userDal.Add(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "Given Dto is null", Messages.err_null);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                var user = _userDal.Get(u => u.Id == id);
                if (user != null)
                {
                    _userDal.Delete(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<User> Get(Expression<Func<User, bool>> filter)
        {
            try
            {
                var user = _userDal.Get(filter);
                if (user != null)
                {
                    return new SuccessDataResult<User>(user, "Ok", Messages.success);
                }
                return new ErrorDataResult<User>(null, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<UserListDto> GetById(int id)
        {
            try
            {
                var user = _userDal.Get(u => u.Id == id);
                if (user != null)
                {
                    return new SuccessDataResult<UserListDto>(new UserListDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                    }, "Ok", Messages.success);
                }
                return new ErrorDataResult<UserListDto>(null, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UserListDto>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<List<UserListDto>> GetList(Expression<Func<User, bool>> filter = null)
        {
            try
            {
                var userList = filter != null ? _userDal.GetList(filter) : _userDal.GetList();
                if (userList != null && userList.Count > 0)
                {
                    var userDtoList = new List<UserListDto>();
                    foreach (var user in userList)
                    {
                        userDtoList.Add(new UserListDto
                        {
                            Id = user.Id,
                            Username = user.Username,
                            Email = user.Email,
                        });
                        return new SuccessDataResult<List<UserListDto>>(userDtoList, "Ok", Messages.success);
                    }
                }
                return new ErrorDataResult<List<UserListDto>>(null, "User list not found", Messages.user_list_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<UserListDto>>(null, ex.Message, Messages.unknown_err);
            }
        }
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            try
            {
                if (user != null)
                {
                    var claims = _userDal.GetClaims(user);
                    return new ErrorDataResult<List<OperationClaim>>(claims, "Ok", Messages.success);
                }
                return new ErrorDataResult<List<OperationClaim>>(null, "Operation claims not found", Messages.operation_claims_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<OperationClaim>>(null, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> Update(UserUpdateDto userCreateDto)
        {
            try
            {
                var user = _userDal.Get(u => u.Id == userCreateDto.Id);
                if(user != null)
                {
                    _userDal.Update(new User
                    {
                        Id = user.Id,
                        Name = userCreateDto.Name,
                        Surname = userCreateDto.Surname,
                        Username = userCreateDto.Username,
                        Email = userCreateDto.Email,
                        PasswordSalt = user.PasswordSalt,
                        PasswordHash = user.PasswordHash,
                    });
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
            }
        }

        public IDataResult<bool> ChangeUserPassword(User user)
        {
            try
            {
                var checkUser = _userDal.Get(u => u.Id == user.Id);
                if(checkUser != null)
                {
                    _userDal.Update(user);
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "User not found", Messages.user_not_found);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<bool>(false, ex.Message, Messages.unknown_err);
                throw;
            }
        }

        //public IDataResult<List<UserListPagingDto>> GetListWithPaging(UserListFilterDto dto)
        //{
        //    try
        //    {
        //        var users = _userDal.GetList().ToList();
        //        if (users!= null)
        //        {
        //            var usersdto = new List<UserListDto>();
        //            foreach (var item in users)
        //            {
        //                usersdto.Add(new UserListDto
        //                {
        //                    Id = item.Id,
        //                    Email = item.Email,
        //                    Username = item.Username,
                            
        //                });
        //            }
        //            if (dto.Search !=null)
        //            {
        //                var searching = dto.Search;
        //                usersdto=usersdto.Where(x=>x.Username.Trim().ToLower().Contains(searching.Trim().ToLower())||x.Email.Trim().ToLower().Contains(searching.Trim().ToLower())).ToList();
        //            }
        //            if (dto.Email !=null)
        //            {
        //                usersdto = usersdto.Where(X => X.Email.Trim().ToLower() == dto.Email.Trim().ToLower()).ToList();
        //            }
        //            if (dto.UserName !=null)
        //            {
        //                usersdto = usersdto.Where(x => x.Username.Trim().ToLower() == dto.UserName.Trim().ToLower()).ToList();
        //            }

        //            var pagingSize = dto.PagingFilterDto.Size;
        //            var pagingNumber = dto.PagingFilterDto.Page;
        //            pagingNumber = usersdto.Count % pagingSize > 0 ? pagingNumber + 1 : pagingNumber;
        //            var totalItem = usersdto.Count;
        //            int totalPage = (int)Math.Ceiling((double)usersdto.Count / pagingNumber);
        //            pagingNumber = pagingNumber <= 1 ? 1 : pagingNumber;

        //            var test = new UserListPagingDto
        //            {
        //                Users = usersdto,
        //                Page = new Entity.DTO.PagingDto
        //                {
        //                    Page= totalPage,
        //                    Size= pagingSize,
                            
        //                    TotalCount= totalItem,  
        //                    TotalPages= totalPage,
        //                }
        //            };
        //            return new SuccessDataResult<List<UserListPagingDto>>(test,"OK", Messages.success);


                
            
          
        
    }
}
