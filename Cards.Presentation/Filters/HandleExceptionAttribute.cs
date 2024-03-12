using Cards.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cards.Presentation.Filters
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                context.Result = (IActionResult?)HandleException(context.Exception);
                context.ExceptionHandled = true;
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            int resultStatusCode;

            if (ex is BadRequestException)
            {
                resultStatusCode = StatusCodes.Status400BadRequest;
            }
            else if (ex is NotFoundException)
            {
                resultStatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                resultStatusCode = StatusCodes.Status500InternalServerError;
            }


            if (ex is BadRequestException badRequestException)
            {
                return new ObjectResult(badRequestException.Message)
                {
                    StatusCode = resultStatusCode
                };
            }

            if (ex is NotFoundException notFoundException)
            {
                return new ObjectResult(notFoundException.Message)
                {
                    StatusCode = resultStatusCode
                };
            }

            return new ObjectResult(ex.Message)
            {
                StatusCode = resultStatusCode
            };

        }
    }
}
