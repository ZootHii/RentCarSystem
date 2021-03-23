using System.Linq;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.TokenCreation;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                EMail = userRegisterDto.EMail,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var result = _userService.Add(user);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            return new SuccessDataResult<User>(Messages.UserRegistered, user);
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var result = _userService.GetUserByEMail(userLoginDto.EMail);
            var userToCheck = result.Data;
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordNotTrue, userToCheck);
            }

            return new SuccessDataResult<User>(Messages.UserLoggedIn, userToCheck);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var result = _userService.GetUserOperationClaims(user);
            var userOperationClaimDtoList = result.Data;
            
            var operationClaimList = 
                userOperationClaimDtoList.Select(userOperationClaimDto => new OperationClaim
                {
                    Id = userOperationClaimDto.OperationClaimId, 
                    Name = userOperationClaimDto.OperationClaimName
                }).ToList();
            
            var accessToken = _tokenHelper.CreateAccessToken(user, operationClaimList);
            return new SuccessDataResult<AccessToken>(Messages.AccessTokenCreated, accessToken);
        }
    }
}