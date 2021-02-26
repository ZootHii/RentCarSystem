using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
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

        public IResult Add(Customer customer)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            /*if (string.IsNullOrEmpty(customer.CompanyName) || customer.CompanyName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }*/
            
            FluentValidationTool.Validate(new CustomerValidator(), customer);
            
            _customerDal.Add(customer);
            return new SuccessResult("added");
        }

        public IResult Update(Customer customer)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            /*if (string.IsNullOrEmpty(customer.CompanyName) || customer.CompanyName == " ")
            {
                return new ErrorResult(Messages.InvalidName);
            }*/
            
            FluentValidationTool.Validate(new CustomerValidator(), customer);
            
            _customerDal.Update(customer);
            return new SuccessResult("updated");
        }

        public IResult Delete(Customer customer)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            _customerDal.Delete(customer);
            return new SuccessResult("deleted");
        }

        public IDataResult<Customer> GetCustomerById(int customerId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<Customer>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<Customer>("get by id",_customerDal.Get(c => c.Id == customerId));
        }

        public IDataResult<List<Customer>> GetAllCustomers()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Customer>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<Customer>>("get all",_customerDal.GetAll());
        }

        public IDataResult<List<Customer>> GetCustomersByUserId(int userId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Customer>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<Customer>>("all user id",_customerDal.GetAll(c => c.UserId == userId));
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<CustomerDetailDto>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<CustomerDetailDto>>("detail",_customerDal.GetCustomerDetails());
        }
    }
}