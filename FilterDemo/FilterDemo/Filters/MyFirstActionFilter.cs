using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo
{
    public class MyFirstActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("first filter start");
            await next();
            Console.WriteLine("first filter finish");
        }
    }
}
