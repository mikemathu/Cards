using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Cards.Presentation.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(
                  "Invalid inputs.Please check your input fields and ensure they are all filled.");
            }
            /*  if (!context.ModelState.IsValid)
                  context.Result = new UnprocessableEntityObjectResult(context.ModelState);*/

        }
    }
}