using System;
using System.Linq;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Aspects.Autofac.SecuredOperation.Jwt
{
    public class SecuredOperationAspect : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public SecuredOperationAspect(string roles)
        {
            _roles = roles.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var claimRoles = _httpContextAccessor.HttpContext?.User.ClaimRoles();
            if (_roles.Any(role => claimRoles != null && claimRoles.Contains(role)))
            {
                return;
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}