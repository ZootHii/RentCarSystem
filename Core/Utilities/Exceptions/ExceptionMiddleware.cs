using System;
using System.Net;
using System.Threading.Tasks;
using Core.Utilities.Exceptions.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            const string message = "Internal Server Error";

            Console.WriteLine(exception.GetType());
            Console.WriteLine(exception.Message);
            
            if (exception.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    Message = exception.Message,
                    StatusCode = httpContext.Response.StatusCode,
                    ValidationErrors = ((ValidationException) exception).Errors
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                Message = exception.Message,
                //Message = message,
                StatusCode = httpContext.Response.StatusCode,
            }.ToString());
        }
        
    }
}