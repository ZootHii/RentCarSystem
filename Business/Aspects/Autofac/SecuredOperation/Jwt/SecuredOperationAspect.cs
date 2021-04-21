using System;
using System.Linq;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Exceptions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
            if (_httpContextAccessor.HttpContext != null)
            {
                var claimRoles = _httpContextAccessor.HttpContext.User.ClaimRoles();
            
                Console.WriteLine(_httpContextAccessor.HttpContext.Request.GetDisplayUrl());
                /*foreach (string role in _roles)
                {
                    Console.WriteLine("Role için : "+role);
                }
                foreach (string claimRole in claimRoles)
                {
                    Console.WriteLine("claim role için : " + claimRole.Length);
                }*/

                foreach (string role in _roles)
                {
                    if (claimRoles.Contains(role))
                    {
                        return;
                    }
                }
            }

            throw new AuthorizationException(Messages.AuthorizationDenied);
        }
    }
}