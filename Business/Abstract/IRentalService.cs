using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        public IResult Add(Rental rental);
        public IResult Update(Rental rental);
        public IResult Delete(Rental rental);
        public IDataResult<Rental> GetRentalById(int rentalId);
        public IDataResult<List<Rental>> GetAllRentals();
        public IDataResult<List<Rental>> GetRentalsByCarId(int carId);
        public IDataResult<List<Rental>> GetRentalsByCustomerId(int customerId);
        public IDataResult<List<RentalDetailDto>> GetRentalsDetails();
        public IResult CheckIfCarCanBeRented(Rental rental);
    }
}