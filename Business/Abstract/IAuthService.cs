using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.TokenCreation;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        public IDataResult<User> Register(UserRegisterDto userForRegisterDto);
        public IDataResult<User> Login(UserLoginDto userForLoginDto);
        public IDataResult<AccessToken> CreateAccessToken(User user);
    }
}