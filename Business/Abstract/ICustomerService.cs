using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        public IResult Add(Customer customer);
        public IResult Update(Customer customer);
        public IResult Delete(Customer customer);
        public IDataResult<Customer> GetCustomerById(int customerId);
        public IDataResult<List<Customer>> GetAllCustomers();
        public IDataResult<List<Customer>> GetCustomersByUserId(int userId);
        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    }
}