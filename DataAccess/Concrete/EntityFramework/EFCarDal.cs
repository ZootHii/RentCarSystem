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
                    from car in context.Cars
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    join carImage in context.CarImages on car.Id equals carImage.CarId
                    
                    select new CarDetailDto
                    {
                        CarId = car.Id,
                        BrandName = brand.BrandName,
                        ColorName = color.ColorName,
                        ModelYear = car.ModelYear,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description,
                        ImagePath = carImage.ImagePath
                    };
                return result.ToList();
            }
        }
    }
}