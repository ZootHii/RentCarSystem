using System;
using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(rental => rental.Id).NotEmpty();
            RuleFor(rental => rental.CarId).NotEmpty();
            RuleFor(rental => rental.CustomerId).NotEmpty();
            RuleFor(rental => rental.RentDate).NotEmpty().WithMessage(Messages.RentalInvalidRentDate);;
            RuleFor(rental => rental.RentDate).GreaterThanOrEqualTo(DateTime.Now).WithMessage(Messages.RentalInvalidRentDate);
            RuleFor(rental => rental.ReturnDate - rental.RentDate).Must(AtLeast2Hours).WithMessage(Messages.RentalInvalidReturnDate);
        }

        private bool AtLeast2Hours(TimeSpan? dateTime)
        {
            bool isValid;
            if (dateTime == null)
            {
                isValid = true;
            }
            else if (dateTime > new TimeSpan(02, 00, 00))
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }
    }
}