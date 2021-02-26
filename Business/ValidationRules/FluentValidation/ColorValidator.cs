using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            //RuleFor(color => color.Id).NotEmpty();
            RuleFor(color => color.ColorName).NotEmpty().WithMessage(Messages.InvalidName);
        }
    }
}