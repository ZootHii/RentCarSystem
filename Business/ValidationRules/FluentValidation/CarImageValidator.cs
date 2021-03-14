using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(image => image.CarId).NotEmpty().WithMessage("Car id should not be empty");
            RuleFor(image => image.ImageName).Empty();
            RuleFor(image => image.UploadDate).Empty();
            RuleFor(image => image.ImagePath).Empty();
        }
    }
}