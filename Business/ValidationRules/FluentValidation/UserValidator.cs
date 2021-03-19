using System.Text.RegularExpressions;
using Business.Constants;
using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly Regex hasNumber = new Regex(@"[0-9]+");
        private readonly Regex hasUpperChar = new Regex(@"[A-Z]+");
        private readonly Regex hasMinimum4Chars = new Regex(@".{4,}");

        public UserValidator()
        {
            RuleFor(user => user.EMail).NotEmpty().WithMessage(Messages.UserInvalidEMail);
            RuleFor(user => user.EMail).EmailAddress().WithMessage(Messages.UserInvalidEMail);
            //RuleFor(user => user.Password).NotEmpty().WithMessage(Messages.UserInvalidPassword);
            //RuleFor(user => user.Password).Must(Password).WithMessage(Messages.UserInvalidPassword);
            RuleFor(user => user.FirstName).NotEmpty().WithMessage(Messages.InvalidName);
            RuleFor(user => user.FirstName).Must(Name).WithMessage(Messages.InvalidName);
            RuleFor(user => user.LastName).NotEmpty().WithMessage(Messages.InvalidName);
            RuleFor(user => user.LastName).Must(Name).WithMessage(Messages.InvalidName);
        }

        public bool Password(string password)
        {
            bool isValid = hasNumber.IsMatch(password)
                           && hasUpperChar.IsMatch(password)
                           && hasMinimum4Chars.IsMatch(password);
            return isValid;
        }

        public bool Name(string name)
        {
            bool isValid = !hasNumber.IsMatch(name);
            return isValid;
        }
    }
}