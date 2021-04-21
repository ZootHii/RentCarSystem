using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Internal;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal, ICustomerService customerService)
        {
            _userDal = userDal;
            _customerService = customerService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var result = BusinessRules.Run(CheckIfUserExistsWithTheSameEMail<object>(user.EMail).IResult);
            if (result != null)
            {
                return result;
            }

            _userDal.Add(user);
            var userForCustomer = _userDal.Get(u => u.EMail == user.EMail);
            _customerService.Add(new Customer
            {
                UserId = userForCustomer.Id,
                CompanyName = "" //  TODO WHEN REGISTER ITS EMPTY USER CAN CHANGE FROM PROFILE
            });
            
            return new SuccessResult(Messages.UserAdded);
        }

        // TODO CONTROL CURRENT PASSWORD
        //[ValidationAspect(typeof(UserValidator))]
        public IResult Update(UserEditDto userEditDto)
        {
            
            var userToUpdate = _userDal.Get(u => u.Id == userEditDto.Id);
            
            if (!HashingHelper.VerifyPasswordHash(userEditDto.CurrentPassword, userToUpdate.PasswordHash, userToUpdate.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordNotTrue);
            }
            
            var user = new User
            {
                Id = userEditDto.Id,
                EMail = userEditDto.EMail,
                FirstName = userEditDto.FirstName,
                LastName = userEditDto.LastName,
                Status = false,
            };
            
            if (!userEditDto.NewPassword.IsNullOrEmpty())
            {
                HashingHelper.CreatePasswordHash(userEditDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            
            // TODO check no change in password
            if (userEditDto.NewPassword.IsNullOrEmpty())
            {
                user.PasswordHash = userToUpdate.PasswordHash;
                user.PasswordSalt = userToUpdate.PasswordSalt;
            }
            
            _userDal.Update(user);

            int customerId = _customerService.GetCustomerByUserId(userToUpdate.Id).Data.Id;
            
            _customerService.Update(new Customer
            {
                Id = customerId,
                UserId = userEditDto.Id,
                CompanyName = userEditDto.CompanyName // TODO WHEN PROFILE EDIT ITS EMPTY USER CAN CHANGE AND UPDATE
            });
            return new SuccessResult(Messages.UserUpdated);
        }

        // TODO CONTROL CURRENT PASSWORD
        public IResult Delete(UserEditDto userEditDto)
        {
            var userToDelete = _userDal.Get(u => u.Id == userEditDto.Id);
            
            if (!HashingHelper.VerifyPasswordHash(userEditDto.CurrentPassword, userToDelete.PasswordHash, userToDelete.PasswordSalt))
            {
                return new ErrorResult(Messages.PasswordNotTrue);
            }

            /*HashingHelper.CreatePasswordHash(userEditDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);*/

            var customerToDelete = _customerService.GetCustomerByUserId(userEditDto.Id).Data;
            _customerService.Delete(customerToDelete);
            
            var user = new User
            {
                Id = userEditDto.Id,
                /*EMail = userEditDto.EMail,
                FirstName = userEditDto.FirstName,
                LastName = userEditDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = false,*/
            };
            
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> GetUserById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(user => user.Id == userId));
        }
        
        
        public IDataResult<UserResponseDto> GetUserResponseByEMail(string eMail)
        {
            var result = _userDal.Get(user => user.EMail == eMail);
            if (result == null)
            {
                return new ErrorDataResult<UserResponseDto>(Messages.UserNotFound);
            }

            var userResponse = new UserResponseDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                EMail = result.EMail
            };
            
            return new SuccessDataResult<UserResponseDto>(userResponse);
        }
        
        public IDataResult<User> GetUserByEMail(string eMail)
        {
            var result = _userDal.Get(user => user.EMail == eMail);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<List<User>> GetAllUsers()
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListed, _userDal.GetAll());
        }

        public IDataResult<List<User>> GetUsersByFirstName(string firstName)
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListedFirstName,
                _userDal.GetAll(u => u.FirstName.Contains(firstName)));
        }

        public IDataResult<List<User>> GetUsersByLastName(string lastName)
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListedLastName,
                _userDal.GetAll(u => u.LastName.Contains(lastName)));
        }

        public IDataResult<List<User>> GetUsersByEMail(string eMail)
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListedEMail,
                _userDal.GetAll(u => u.EMail.Contains(eMail)));
        }

        public IDataResult<List<OperationClaimDto>> GetUserOperationClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaimDto>>(_userDal.GetUserOperationClaims(user));
        }

        public IDataResult<List<string>> GetLoggedUserOperationClaims()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var claimRolesList = _httpContextAccessor.HttpContext.User.ClaimRoles();
                return new SuccessDataResult<List<string>>("Successful claims", claimRolesList);
            }

            return new ErrorDataResult<List<string>>("Error Claim");
        }

        public (IResult IResult, IDataResult<T> IDataResult) CheckIfUserExistsWithTheSameEMail<T>(string eMail)
        {
            var result = GetUserByEMail(eMail);
            if (result.Success)
            {
                return (new ErrorResult(Messages.UserExistsWithTheSameEMail), new ErrorDataResult<T>(Messages.UserExistsWithTheSameEMail));
            }

            return (new SuccessResult(), new SuccessDataResult<T>());
        }
    }
}