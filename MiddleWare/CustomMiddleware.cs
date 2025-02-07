
using Microsoft.AspNetCore.Builder;

namespace WebAPI_Learn.MiddleWare
{
    public class Custom1Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Hello I am from custom 1 middleware");
            //await context.Response.WriteAsync("Hello I am from custom 1 middleware");
            await next(context);
        }
    }

    public class Custom2Middleware : IMiddleware //extension method is not there for this class
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Hello I am from custom 2 middleware");
            //await context.Response.WriteAsync("Hello I am from custom 2 middleware");
            await next(context);//next() call in middleware refers to the next middleware 
        }
    }

    public static class ClassWithNoImplementationMiddleware
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Custom1Middleware>();
        }
    }
}
    