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

                switch (contextFeature.Error)
                {

                    case BadRequestException or ArgumentException:
                        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case NotFoundException:
                        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    case EmailAlreadyExistsException:
                        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                        break;
                    case CreateUserFailedException:
                        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                    default:
                        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                ErrorDetails errorDetails;

                if (exception.InnerException is not null)
                {
                    errorDetails  = new ErrorDetails
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

                await httpContext.Response.WriteAsync(errorDetails.ToString());
            }

            return true;
        }
    }
}
