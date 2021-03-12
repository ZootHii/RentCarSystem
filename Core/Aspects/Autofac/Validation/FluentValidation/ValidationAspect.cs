using System;
using System.Linq;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation.FluentValidation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception("Wrong validator type");
            }
            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator) Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType?.GetGenericArguments()[0];
            
            // TODO does not work if IFormFile is null, and I am not sure that I need it to be a list
            //var entities = invocation.Arguments.Where(o => o.GetType() == entityType);
            //foreach (var entity in entities)
            //{
            // ValidationTool.Validate(validator, entity);
            //}
            
            var entity = invocation.Arguments.First(o => o.GetType() == entityType);
            ValidationTool.Validate(validator, entity);
        }
    }
}