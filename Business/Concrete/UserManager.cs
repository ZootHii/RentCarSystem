using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        // validation
        private readonly Regex hasNumber = new Regex(@"[0-9]+");
        private readonly Regex hasUpperChar = new Regex(@"[A-Z]+");
        private readonly Regex hasMinimum4Chars = new Regex(@".{4,}");
        private readonly EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();
        private bool isValid;
        
        // injection
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            isValid = emailAddressAttribute.IsValid(user.EMail);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidEMail);
            }
            
            isValid = hasNumber.IsMatch(user.Password) 
                      && hasUpperChar.IsMatch(user.Password) 
                      && hasMinimum4Chars.IsMatch(user.Password);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidPassword);
            }

            isValid = !hasNumber.IsMatch(user.FirstName);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidNameDigits);
            }

            if (string.IsNullOrEmpty(user.FirstName) || user.FirstName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }

            isValid = !hasNumber.IsMatch(user.LastName);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidNameDigits);
            }
            
            if (string.IsNullOrEmpty(user.LastName) || user.LastName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }
            
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Update(User user)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            isValid = emailAddressAttribute.IsValid(user.EMail);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidEMail);
            }
            
            isValid = hasNumber.IsMatch(user.Password) && hasUpperChar.IsMatch(user.Password) && hasMinimum4Chars.IsMatch(user.Password);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidPassword);
            }

            isValid = !hasNumber.IsMatch(user.FirstName);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidNameDigits);
            }

            if (string.IsNullOrEmpty(user.FirstName) || user.FirstName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }
            
            isValid = !hasNumber.IsMatch(user.LastName);
            if (!isValid)
            {
                return new ErrorResult(Messages.UserInvalidNameDigits);
            }
            
            if (string.IsNullOrEmpty(user.LastName) || user.LastName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
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