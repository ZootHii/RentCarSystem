using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (var context = new CarRentalContext())
            {
                var result = 
                    from c in context.Cars
                    join b in context.Brands on c.BrandId equals b.Id
                    join cl in context.Colors on c.ColorId equals cl.Id
                    select new CarDetailDto
                    {
                        CarId = c.Id,
                        BrandName = b.BrandName,
                        ColorName = cl.ColorName,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description
                    };
                return result.ToList();
            }
        }
    }
}