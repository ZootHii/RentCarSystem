using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
    // TODO define necessary messages
    // TODO do all stuff
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public IResult Update(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public IResult Delete(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<Customer> GetCustomerById(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<Customer>> GetAllCustomers()
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<Customer>> GetCustomersByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}