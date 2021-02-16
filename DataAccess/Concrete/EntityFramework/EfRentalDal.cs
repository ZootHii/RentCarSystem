using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result =
                    from rental in context.Rentals
                    join car in context.Cars on rental.CarId equals car.Id
                    join customer in context.Customers on rental.CustomerId equals customer.Id
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    join user in context.Users on customer.UserId equals user.Id
                    select new RentalDetailDto
                    {
                        Id = rental.Id,
                        CarId = rental.CarId,
                        CustomerId = rental.CustomerId,
                        RentDate = rental.ReturnDate,
                        BrandName = brand.BrandName,
                        ModelYear = car.ModelYear,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EMail = user.EMail,
                        CompanyName = customer.CompanyName
                    };
                return result.ToList();
            }
        }
    }
}