using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        public List<Car> GetAll();
        public void Update(Car car);
    }
}