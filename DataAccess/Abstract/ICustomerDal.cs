using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IEntityRepository<Customer>
    {
        public List<CustomerDetailDto> GetCustomersDetails();
    }
}