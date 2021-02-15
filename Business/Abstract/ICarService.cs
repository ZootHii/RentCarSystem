using System.Collections.Generic;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        public void Add(Car car);
        public void Update(Car car);
        public void Delete(Car car);
        public Car GetCarById(int carId);
        public List<Car> GetAllCars();
        public List<Car> GetCarsByBrandId(int brandId);
        public List<Car> GetCarsByColorId(int colorId);
        public List<CarDetailDto> GetCarDetails();
    }
}