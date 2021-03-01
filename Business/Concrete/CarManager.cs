using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
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

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            /*if (car.ModelYear.Year <= 1999)
            {
                return new ErrorResult(Messages.CarInvalidModelYear);
            }

            if (car.DailyPrice <= 0)
            {
                return new ErrorResult(Messages.CarInvalidDailyPrice);
            }*/
            
            //ValidationTool.Validate(new CarValidator(), car); done with AOP attribute

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            /*if (car.ModelYear.Year <= 1999)
            {
                return new ErrorResult(Messages.CarInvalidModelYear);
            }

            if (car.DailyPrice <= 0)
            {
                return new ErrorResult(Messages.CarInvalidDailyPrice);
            }*/
            
            //ValidationTool.Validate(new CarValidator(), car); done with AOP
            
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IResult Delete(Car car)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<Car> GetCarById(int carId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<Car>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == carId));
        }

        public IDataResult<List<Car>> GetAllCars()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Car>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<Car>>(Messages.CarsListed, _carDal.GetAll());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Car>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<Car>>(Messages.CarsListedBrand, _carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Car>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<Car>>(Messages.CarsListedColor, _carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.SystemMaintenance);
            }
            
            return new SuccessDataResult<List<CarDetailDto>>(Messages.CarsListedDetails, _carDal.GetCarDetails());
        }
    }
}