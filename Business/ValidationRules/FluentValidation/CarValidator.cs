using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Id).NotEmpty();
            RuleFor(car => car.BrandId).NotEmpty();
            RuleFor(car => car.ColorId).NotEmpty();
            RuleFor(car => car.ModelYear.Year).NotEmpty();
            RuleFor(car => car.ModelYear.Year).GreaterThan(1999).WithMessage(Messages.CarInvalidModelYear);
            RuleFor(car => car.DailyPrice).NotEmpty();
            RuleFor(car => car.DailyPrice).GreaterThanOrEqualTo(0).WithMessage(Messages.CarInvalidDailyPrice);
        }
    }
}