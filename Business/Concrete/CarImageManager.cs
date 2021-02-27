using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        
        public IResult Add(CarImage carImage)
        {
            throw new System.NotImplementedException();
        }

        public IResult Update(CarImage carImage)
        {
            throw new System.NotImplementedException();
        }

        public IResult Delete(CarImage carImage)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<Car> GetCarImageById(int carImageId)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetAllCarImages()
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            throw new System.NotImplementedException();
        }
    }
}