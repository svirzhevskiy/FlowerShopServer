using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var env = context.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<ExceptionFilter>)) as ILogger<ExceptionFilter>;

            var message = "Error occured";
            var errorCode = ErrorCode.None;

            if (context.Exception is AppError ex)
            {
                if (ex.Code == ErrorCode.Bug)
                {
                    logger.LogCritical(ex.Message);
                }

                message = ex.Message;
                errorCode = ex.Code;
            }
            else
            {
                var devMessage = $"{context.Exception.Message}\n{context.Exception.StackTrace}";
                logger.LogError($"ErrorFilter {devMessage}");

                if (env.IsDevelopment())
                {
                    message = devMessage;
                }
            }

            var json = new
            {
                error_message = message,
                error_code = errorCode
            };

            context.Result = new JsonResult(json)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
