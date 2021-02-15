using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        public List<CarDetailDto> GetCarDetails();
    }
}