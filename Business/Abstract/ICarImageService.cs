using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        public IResult Add(CarImage carImage);
        public IResult Update(CarImage carImage);
        public IResult Delete(CarImage carImage);
        public IDataResult<Car> GetCarImageById(int carImageId);
        public IDataResult<List<CarImage>> GetAllCarImages();
        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId);
    }
}