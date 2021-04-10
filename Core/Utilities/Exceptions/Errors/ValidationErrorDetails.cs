using System.Collections.Generic;
using FluentValidation.Results;

namespace Core.Utilities.Exceptions.Errors
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
    }
}