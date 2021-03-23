using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
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
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            var result = BusinessRules.Run(CheckIfUserExistsWithTheSameEMail<object>(user.EMail).IResult);
            if (result != null)
            {
                return result;
            }

            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> GetUserById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(user => user.Id == userId));
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