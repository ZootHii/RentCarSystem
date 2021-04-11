using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        public IResult Add(User user);
        //public IResult Update(User user);
        public IResult Update(UserEditDto userEditDto);
        public IResult Delete(UserEditDto userEditDto);
        //public IResult Delete(User user);
        public IDataResult<User> GetUserById(int userId);
        public IDataResult<UserResponseDto> GetUserResponseByEMail(string eMail);
        public IDataResult<User> GetUserByEMail(string eMail);
        public IDataResult<List<User>> GetAllUsers();
        public IDataResult<List<User>> GetUsersByFirstName(string firstName);
        public IDataResult<List<User>> GetUsersByLastName(string lastName);
        public IDataResult<List<User>> GetUsersByEMail(string eMail);
        public IDataResult<List<OperationClaimDto>> GetUserOperationClaims(User user);

        /*#region Rules
        /// <summary>
        /// If there is a user with the same eMail returns ErrorResult otherwise returns SuccessResult
        /// </summary>
        public (IResult IResult, IDataResult<T> IDataResult) CheckIfUserExistsWithTheSameEMail<T>(string eMail);

        #endregion*/
    }
}