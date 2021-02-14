using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        public List<Car> GetAll();
        public void Update(Car car);
        public List<Car> GetAllByBrandId(int brandId);
        public List<Car> GetAllByColorId(int colorId);
        public Car GetById(int carId);
    }
}