using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        public IResult Add(Car car);
        public IResult Update(Car car);
        public IResult Delete(Car car);
        public IDataResult<Car> GetCarById(int carId);
        public IDataResult<List<Car>> GetAllCars();
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        public IDataResult<List<Car>> GetCarsByColorId(int colorId);
        public IDataResult<List<CarDetailDto>> GetCarsDetails();
        public IDataResult<List<CarDetailDto>> GetCarsDetailsWithPreviewFirstImage();
        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandId(int brandId);
        public IDataResult<List<CarDetailDto>> GetCarsDetailsByColorId(int colorId);
        public IDataResult<CarDetailDto> GetCarDetailsByCarId(int carId);
        
    }
}