using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.DbContexts;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentalContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result =
                    from customer in context.Customers
                    join user in context.Users on customer.UserId equals user.Id
                    select new CustomerDetailDto
                    {
                        Id = customer.Id,
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EMail = user.EMail,
                        CompanyName = customer.CompanyName
                    };
                return result.ToList();
            }
        }

        public CustomerDetailDto GetCustomerDetailsByUserId(int userId)
        {
            using (var context = new CarRentalContext())
            {
                var result =
                    from customer in context.Customers
                    join user in context.Users on customer.UserId equals user.Id
                    where customer.UserId == userId
                    select new CustomerDetailDto
                    {
                        Id = customer.Id,
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EMail = user.EMail,
                        CompanyName = customer.CompanyName
                    };
                return result.SingleOrDefault();
            }
        }
    }
}