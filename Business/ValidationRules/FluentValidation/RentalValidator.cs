using System;
using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        private readonly DateTime now = DateTime.Now;

        /*public RentalValidator()
        {
            RuleFor(rental => rental.CarId).NotEmpty();
            RuleFor(rental => rental.CustomerId).NotEmpty();
            RuleFor(rental => rental.RentDate).NotEmpty().WithMessage(Messages.RentalInvalidRentDate);
            RuleFor(rental => rental.RentDate).GreaterThanOrEqualTo(now.AddSeconds(-now.Second))
                .WithMessage(Messages.RentalInvalidRentDate);
        }*/
    }
}