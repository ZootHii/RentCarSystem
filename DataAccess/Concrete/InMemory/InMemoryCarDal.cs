using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car {Id = 1, BrandId = 1, ColorId = 1, ModelYear = new DateTime(2001,01,01), DailyPrice = 99.99M, Description = "Temiz"},
                new Car {Id = 2, BrandId = 2, ColorId = 1, ModelYear = new DateTime(2007,01,01), DailyPrice = 199.99M, Description = "Temiz"},
                new Car {Id = 3, BrandId = 1, ColorId = 2, ModelYear = new DateTime(2018,01,01), DailyPrice = 299.99M, Description = "Temiz"},
            };
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.Id == id).ToList();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car) // Maybe I can send it as int id by this way no need reference thing
        {
            var toUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            if (toUpdate != null)
            {
                toUpdate.BrandId = car.BrandId;
                toUpdate.ColorId = car.ColorId;
                toUpdate.ModelYear = car.ModelYear;
                toUpdate.DailyPrice = car.DailyPrice;
                toUpdate.Description = car.Description;
            }
        }

        public void Delete(Car car)
        {
            var toDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(toDelete);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarsDetails()
        {
            throw new NotImplementedException();
        }
    }
}