using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.TokenCreation;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        public IDataResult<UserResponseDto> Register(UserRegisterDto userForRegisterDto);
        public IDataResult<UserResponseDto> Login(UserLoginDto userForLoginDto);
        public IDataResult<CustomerResponseDto> LoginCustomer(UserLoginDto userForLoginDto);
        /*public IDataResult<AccessToken> CreateAccessToken(User user);*/
    }
}