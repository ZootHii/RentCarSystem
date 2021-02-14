using System;
using System.Collections.Generic;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        
        public void Add(Car car)
        {
            
            if (car.ModelYear.Year >= 1999 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                Console.WriteLine("Car successfully added!");
            }
            else
            {
                Console.WriteLine("Car ModelYear must be bigger than 1999 and DailyPrice must be bigger than 0");
            }
        }

        public void Update(Car car)
        {
            if (car.ModelYear.Year >= 1999 && car.DailyPrice > 0)
            {
                _carDal.Update(car);
                Console.WriteLine("Car successfully updated!");
            }
            else
            {
                Console.WriteLine("Car ModelYear must be bigger than 1999 and DailyPrice must be bigger than 0");
            }
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
            Console.WriteLine("Car successfully deleted!");
        }

        public Car GetCarById(int carId)
        {
            return _carDal.Get(c => c.Id == carId);
        }

        public List<Car> GetAllCars()
        {
            return _carDal.GetAll();
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }
    }
}