using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        // TODO check is e mail in use
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            var result = BusinessRules.Run(CheckIfEMailInUse(user.EMail));
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
            var result = BusinessRules.Run(CheckIfEMailInUse(user.EMail));
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
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }

        public IDataResult<List<User>> GetAllUsers()
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListed, _userDal.GetAll());
        }

        public IDataResult<List<User>> GetUsersByFirstName(string firstName)
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListedFirstName, _userDal.GetAll(u => u.FirstName.Contains(firstName)));
        }

        public IDataResult<List<User>> GetUsersByLastName(string lastName)
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListedLastName, _userDal.GetAll(u => u.LastName.Contains(lastName)));
        }

        public IDataResult<List<User>> GetUsersByEMail(string eMail)
        {
            return new SuccessDataResult<List<User>>(Messages.UsersListedEMail, _userDal.GetAll(u => u.EMail.Contains(eMail)));
        }

        #region Rules
        
        private IResult CheckIfEMailInUse(string EMail)
        {
            var result = _userDal.GetAll(u => u.EMail == EMail).Any();
            if (result)
            {
                return new ErrorResult(Messages.UserEMailInUse);
            }

            return new SuccessResult("ddd");
        }
        
        #endregion
    }
}