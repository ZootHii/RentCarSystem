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
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId && (r.ReturnDate == null || r.ReturnDate >= DateTime.Now));
    
            if (result.Count != 0)
            {
                return new ErrorResult(Messages.RentalsCarInUse);
            }
            
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            /*if (rental.ReturnDate != null)
            {
                var dateTime = rental.ReturnDate - rental.RentDate;
                if (dateTime < new TimeSpan(02,00,00))
                {
                    return new ErrorResult(Messages.RentalInvalidReturnDate);
                }
            }*/

            /*if (rental.RentDate < DateTime.Now)
            {
                return new ErrorResult();
            }*/
            
            //ValidationTool.Validate(new RentalValidator(), rental);
            
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }

            /*if (rental.ReturnDate != null)
            {
                var dateTime = rental.ReturnDate - rental.RentDate;
                if (dateTime < new TimeSpan(02,00,00))
                {
                    return new ErrorResult(Messages.RentalInvalidReturnDate);
                }
            }*/
            
            //ValidationTool.Validate(new RentalValidator(), rental);
            
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorResult(Messages.SystemMaintenance);
            }
            
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<Rental> GetRentalById(int rentalId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<Rental>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }

        public IDataResult<List<Rental>> GetAllRentals()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Rental>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Rental>>(Messages.RentalsListed, _rentalDal.GetAll());
        }

        public IDataResult<List<Rental>> GetRentalsByCarId(int carId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Rental>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Rental>>(Messages.RentalsListedCar,
                _rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetRentalsByCustomerId(int customerId)
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Rental>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<Rental>>(Messages.RentalsListedCustomer,
                _rentalDal.GetAll(r => r.CustomerId == customerId));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.SystemMaintenance);
            }

            return new SuccessDataResult<List<RentalDetailDto>>(Messages.RentalsListedDetails,
                _rentalDal.GetRentalDetails());
        }
    }
}