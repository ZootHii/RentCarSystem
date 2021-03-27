using System.Collections.Generic;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
    // TODO define necessary messages
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult("added");
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult("updated");
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult("deleted");
        }

        public IDataResult<Customer> GetCustomerById(int customerId)
        {
            return new SuccessDataResult<Customer>("get by id", _customerDal.Get(c => c.Id == customerId));
        }

        public IDataResult<List<Customer>> GetAllCustomers()
        {
            return new SuccessDataResult<List<Customer>>("get all", _customerDal.GetAll());
        }

        public IDataResult<List<Customer>> GetCustomersByUserId(int userId)
        {
            return new SuccessDataResult<List<Customer>>("all user id", _customerDal.GetAll(c => c.UserId == userId));
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomersDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>("detail", _customerDal.GetCustomersDetails());
        }
    }
}