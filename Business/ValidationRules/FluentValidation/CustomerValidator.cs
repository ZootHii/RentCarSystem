﻿using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.UserId).NotEmpty();
            RuleFor(customer => customer.CompanyName).NotEmpty().WithMessage(Messages.InvalidName);
        }
    }
}