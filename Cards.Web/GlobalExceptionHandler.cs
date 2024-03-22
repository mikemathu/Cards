using Cards.Domain.ErrorModel;
using Cards.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

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

                if (contextFeature.Error is BadRequestException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else if (contextFeature.Error is NotFoundException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else if (contextFeature.Error is EmailAlreadyExistsException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                }
                else if (contextFeature.Error.InnerException is not null &&
                         contextFeature.Error.InnerException.Message.Contains("color"))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }


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
