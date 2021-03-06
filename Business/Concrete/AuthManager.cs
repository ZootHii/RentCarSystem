﻿using System;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
        private readonly ICustomerService _customerService;
        
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICustomerService customerService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _customerService = customerService;
        }
        
        public IDataResult<UserResponseDto> Register(UserRegisterDto userRegisterDto)
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
            
            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EMail = user.EMail,
            };
            
            if (!result.Success)
            {
                userResponse.AccessToken = null;
                return new ErrorDataResult<UserResponseDto>(userResponse);
            }
            
            userResponse.AccessToken = CreateAccessTokenMine(user);
            return new SuccessDataResult<UserResponseDto>(Messages.UserRegistered, userResponse);
        }
        
        
        public IDataResult<UserResponseDto> Login(UserLoginDto userLoginDto)
        {
            var result = _userService.GetUserByEMail(userLoginDto.EMail);
            var userToCheck = result.Data;

            if (!result.Success)
            {
                return new ErrorDataResult<UserResponseDto>(result.Message);
            }
            
            var userResponse = new UserResponseDto
            {
                Id = userToCheck.Id,
                FirstName = userToCheck.FirstName,
                LastName = userToCheck.LastName,
                EMail = userToCheck.EMail,
            };
            
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                userResponse.AccessToken = null;
                return new ErrorDataResult<UserResponseDto>(Messages.PasswordNotTrue, userResponse);
            }

            userResponse.AccessToken = CreateAccessTokenMine(userToCheck);
            return new SuccessDataResult<UserResponseDto>(Messages.UserLoggedIn, userResponse);
        }

        [ValidationAspect(typeof(UserLoginValidator))]
        public IDataResult<CustomerResponseDto> LoginCustomer(UserLoginDto userLoginDto)
        {
            var resultUserByEMail = _userService.GetUserByEMail(userLoginDto.EMail);
            if (!resultUserByEMail.Success)
            {
                return new ErrorDataResult<CustomerResponseDto>(resultUserByEMail.Message);
            }
            var userToCheck = resultUserByEMail.Data;
            
            var resultCustomerDetailsByUserId = _customerService.GetCustomerDetailsByUserId(userToCheck.Id);
            if (!resultCustomerDetailsByUserId.Success)
            {
                return new ErrorDataResult<CustomerResponseDto>(resultCustomerDetailsByUserId.Message);
            }

            var customerToReturn = resultCustomerDetailsByUserId.Data;
            
            var customerResponse = new CustomerResponseDto()
            {
                Id = customerToReturn.Id,
                UserId = customerToReturn.UserId,
                FirstName = customerToReturn.FirstName,
                LastName = customerToReturn.LastName,
                EMail = customerToReturn.EMail,
                CompanyName = customerToReturn.CompanyName,
            };
            
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                customerResponse.AccessToken = null;
                return new ErrorDataResult<CustomerResponseDto>(Messages.PasswordNotTrue, customerResponse);
            }

            customerResponse.AccessToken = CreateAccessTokenMine(userToCheck);
            return new SuccessDataResult<CustomerResponseDto>(Messages.UserLoggedIn, customerResponse);
        }

        /*public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            /*Console.WriteLine(user.Id);
            Console.WriteLine(user.Status);
            Console.WriteLine(user.EMail);
            Console.WriteLine(user.FirstName);
            Console.WriteLine(user.LastName);
            Console.WriteLine(user.PasswordHash);
            Console.WriteLine(user.PasswordSalt);
            Console.WriteLine(user);#1#
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
        }*/
        
        private AccessToken CreateAccessTokenMine(User user)
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
            return accessToken;
        }
    }
}