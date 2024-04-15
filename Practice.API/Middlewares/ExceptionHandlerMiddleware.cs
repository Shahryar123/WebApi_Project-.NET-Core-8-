using System.Net;

namespace Practice.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger , RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex) 
            {
                var errorid = Guid.NewGuid();

                logger.LogError($"{errorid} : {ex.Message}");

                // now we used globalexceptionhandling if there is any error in the code this exception occur

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorid,
                    Message = "Something Went Wrong! We are looking into it"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
