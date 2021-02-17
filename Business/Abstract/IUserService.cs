using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        public IResult Add(User user);
        public IResult Update(User user);
        public IResult Delete(User user);
        public IDataResult<User> GetUserById(int userId);
        public IDataResult<List<User>> GetAllUsers();
        public IDataResult<List<User>> GetUsersByFirstName(string firstName);
        public IDataResult<List<User>> GetUsersByLastName(string lastName);
        public IDataResult<List<User>> GetUsersByEMail(string eMail);
    }
}