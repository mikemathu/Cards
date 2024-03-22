using Cards.Domain.ErrorModel;
using Cards.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace Cards.Web
{
    public class GlobalExceptionHandler : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (contextFeature != null)
            {

                httpContext.Response.StatusCode = contextFeature.Error switch
                {
                    BadRequestException or ArgumentException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    EmailAlreadyExistsException => StatusCodes.Status409Conflict,
                    CreateUserFailedException => StatusCodes.Status500InternalServerError,
                    _ => StatusCodes.Status500InternalServerError,
                };
                ErrorDetails errorDetails;

                if (exception.InnerException is not null)
                {
                    errorDetails = new ErrorDetails
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        Message = exception.InnerException.Message
                    };
                }
                else
                {
                    errorDetails = new ErrorDetails
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        Message = exception.Message
                    };
                }
                await httpContext.Response.WriteAsync(errorDetails.ToString(), cancellationToken: cancellationToken);
            }

            return true;
        }
    }
}
