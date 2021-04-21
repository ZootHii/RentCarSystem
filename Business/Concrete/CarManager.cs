using System.Collections.Generic;
using Business.Abstract;
using Business.Aspects.Autofac.SecuredOperation.Jwt;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private ICarImageService _carImageService;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }

        [SecuredOperationAspect("car.add, admin")]
        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperationAspect("car.update, admin")]
        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        [TransactionScopeAspect]
        [SecuredOperationAspect("car.delete, admin")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect(10)]
        public IDataResult<Car> GetCarById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == carId));
        }
        
        //[SecuredOperationAspect("admin")]
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAllCars()
        {
            return new SuccessDataResult<List<Car>>(Messages.CarsListed, _carDal.GetAll());
        }

        [CacheAspect(30)]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(Messages.CarsListedBrand,
                _carDal.GetAll(c => c.BrandId == brandId));
        }

        [CacheAspect(30)]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(Messages.CarsListedColor,
                _carDal.GetAll(c => c.ColorId == colorId));
        }

        [CacheAspect(30)]
        public IDataResult<List<CarDetailDto>> GetCarsDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(Messages.CarsListedDetails, _carDal.GetCarsDetails());
        }
        
        [CacheAspect(30)]
        public IDataResult<List<CarDetailDto>> GetCarsDetailsWithPreviewFirstImage()
        {
            var carsDetails = _carDal.GetCarsDetails();
            var carsDetailsWithPreviewFirstImage = SetPreviewFirstImage(carsDetails);
            
            return new SuccessDataResult<List<CarDetailDto>>(Messages.CarsListedDetails, carsDetailsWithPreviewFirstImage);
        }
        

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>("", _carDal.GetCarsDetailsByBrandId(brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>("", _carDal.GetCarsDetailsByColorId(colorId));
        }
        
        [CacheAspect]
        public IDataResult<CarDetailDto> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<CarDetailDto>("", _carDal.GetCarDetailsByCarId(carId));
        }


        public List<CarDetailDto> SetPreviewFirstImage(List<CarDetailDto> carDetails)
        {
            foreach (var carDetail in carDetails)
            {
                var carPreviewFirstImageByCarId= _carImageService.GetCarPreviewFirstImageByCarId(carDetail.Id);
                carDetail.PreviewFirstImage = carPreviewFirstImageByCarId.Data.ImagePath;
            }

            return carDetails;
        }
    }
}