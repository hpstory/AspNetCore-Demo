using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public IHostEnvironment Environment { get; }

        public MyExceptionFilter(IHostEnvironment environment)
        {
            Environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();
            if (Environment.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.ToString();
            }
            else
            {
                error.Message = "server error";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
