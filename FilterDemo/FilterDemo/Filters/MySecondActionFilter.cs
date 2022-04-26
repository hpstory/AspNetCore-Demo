using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo
{
    public class MySecondActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("second filter start");
            await next();
            Console.WriteLine("second filter finish");
        }
    }
}
