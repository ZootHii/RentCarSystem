using Entities.Concrete.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(userLogin => userLogin.EMail).NotEmpty();
            RuleFor(userLogin => userLogin.EMail).NotNull();
            RuleFor(userLogin => userLogin.EMail).EmailAddress();
            RuleFor(userLogin => userLogin.Password).NotEmpty();
            RuleFor(userLogin => userLogin.Password).NotNull();
        }
    }
}