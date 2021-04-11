using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
        private readonly ICustomerService _customerService;
        private readonly IFindeksService _findeksService;

        public RentalManager(IRentalDal rentalDal, ICustomerService customerService, IFindeksService findeksService)
        {
            _rentalDal = rentalDal;
            _customerService = customerService;
            _findeksService = findeksService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfCarInUse(rental.CarId),
                CheckIfRentalTimeSpanCorrect(rental),
                CheckIfCustomerFindeksScoreEnoughForCar(rental.CustomerId, rental.CarId));
            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfCarInUse(rental.CarId),
                CheckIfRentalTimeSpanCorrect(rental),
                CheckIfCustomerFindeksScoreEnoughForCar(rental.CustomerId, rental.CarId));
            if (result != null)
            {
                return result;
            }

            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<Rental> GetRentalById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }

        public IDataResult<List<Rental>> GetAllRentals()
        {
            return new SuccessDataResult<List<Rental>>(Messages.RentalsListed, _rentalDal.GetAll());
        }

        public IDataResult<List<Rental>> GetRentalsByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(Messages.RentalsListedCar,
                _rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetRentalsByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(Messages.RentalsListedCustomer,
                _rentalDal.GetAll(r => r.CustomerId == customerId));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalsDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(Messages.RentalsListedDetails,
                _rentalDal.GetRentalsDetails());
        }

        #region Rules

        private IResult CheckIfCarInUse(int carId)
        {
            var result = _rentalDal
                .GetAll(r => r.CarId == carId && (r.ReturnDate == null || r.ReturnDate >= DateTime.Now)).Any();
            if (result)
            {
                return new ErrorResult(Messages.RentalsCarInUse);
            }

            return new SuccessResult();
        }

        private IResult CheckIfRentalTimeSpanCorrect(Rental rental) // at least 2 hours
        {
            var rentalTimeSpan = rental.ReturnDate - rental.RentDate;
            bool isCorrect;
            if (rentalTimeSpan == null)
            {
                isCorrect = true;
            }
            else if (rentalTimeSpan > new TimeSpan(02, 00, 00))
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }

            if (!isCorrect)
            {
                return new ErrorResult(Messages.RentalInvalidReturnDate);
            }

            return new SuccessResult();
        }

        
        private IResult CheckIfCustomerFindeksScoreEnoughForCar(int customerId, int carId)
        {
            var customerById = _customerService.GetCustomerById(customerId);
            if (customerById.Success)
            {
                var result = _findeksService.CheckUserFindeksScoreEnoughForCar(carId, customerById.Data.UserId);
                return result;
            }

            return new ErrorResult("CheckIfCustomerFindeksScoreEnoughForCar-> RENTAL MANAGER");
        }
        

        #endregion
    }
}