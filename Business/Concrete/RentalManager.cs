using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            var dateTime = rental.ReturnDate - rental.RentDate;
            if (dateTime.Days <= 0 && dateTime.Hours <= 2 && dateTime.Minutes <= 0)
            {
                return new ErrorResult(Messages.RentalInvalidReturnDate);
            }
            
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Update(Rental rental)
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            var dateTime = rental.ReturnDate - rental.RentDate;
            if (dateTime.Days <= 0 && dateTime.Hours <= 2 && dateTime.Minutes <= 0)
            {
                return new ErrorResult(Messages.RentalInvalidReturnDate);
            }
            
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<Rental> GetRentalById(int rentalId)
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorDataResult<Rental>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }

        public IDataResult<List<Rental>> GetAllRentals()
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorDataResult<List<Rental>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Rental>>(Messages.RentalsListed, _rentalDal.GetAll());
        }

        public IDataResult<List<Rental>> GetRentalsByCarId(int carId)
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorDataResult<List<Rental>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Rental>>(Messages.RentalsListedCar,
                _rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetRentalsByCustomerId(int customerId)
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorDataResult<List<Rental>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Rental>>(Messages.RentalsListedCustomer,
                _rentalDal.GetAll(r => r.CustomerId == customerId));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            if (DateTime.Now.Hour >= 19)
            {
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<RentalDetailDto>>(Messages.RentalsListedDetails,
                _rentalDal.GetRentalDetails());
        }
    }
}