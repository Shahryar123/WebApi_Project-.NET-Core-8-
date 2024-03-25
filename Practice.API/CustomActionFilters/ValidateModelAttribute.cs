using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Practice.API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        // WHAT THIS FUNCTION HAS DONE IS CHECK THE MODEL STATE VALIDTION AND I FAIL RETURN BAD REQUEST
        // TO THE CONTROLLER
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
