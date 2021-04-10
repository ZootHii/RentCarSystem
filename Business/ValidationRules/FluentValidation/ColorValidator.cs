using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            /*RuleFor(color => color.ColorName).MinimumLength(5);
            RuleFor(color => color.ColorName).Must(StartsWithA);
            RuleFor(color => color.ColorName).Must(EndsWithB);*/
            RuleFor(color => color.ColorName).NotEmpty().WithMessage(Messages.InvalidName);
        }

        /*private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A");
        }

        private bool EndsWithB(string arg)
        {
            return arg.EndsWith("b");
        }*/
    }
}