using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
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
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult Delete(User user)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> GetUserById(int userId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<User>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }

        public IDataResult<List<User>> GetAllUsers()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<User>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<User>>(Messages.UsersListed, _userDal.GetAll());
        }

        public IDataResult<List<User>> GetUsersByFirstName(string firstName)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<User>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<User>>(Messages.UsersListedFirstName, _userDal.GetAll(u => u.FirstName.Contains(firstName)));
        }

        public IDataResult<List<User>> GetUsersByLastName(string lastName)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<User>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<User>>(Messages.UsersListedLastName, _userDal.GetAll(u => u.LastName.Contains(lastName)));
        }

        public IDataResult<List<User>> GetUsersByEMail(string eMail)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<User>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<User>>(Messages.UsersListedEMail, _userDal.GetAll(u => u.EMail.Contains(eMail)));
        }
    }
}