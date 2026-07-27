//namespace BookStore.Models
//{
//    public static class customMiddleWare
//    {
//        public static IApplicationBuilder MyOwnMiddleware(this IApplicationBuilder app) 
//        {
//            return app.UseMiddleware<MyCustomeMiddleware>();
//        }
//    }
//    public class MyCustomeMiddleware : IMiddleware
//    {
//        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
//        {
//            await context.Response.WriteAsync("Custom Middleware Start");
//            await context.Response.WriteAsync("\n\n");
//            await context.Response.WriteAsync("Custome Middleware End");

//        }
//    }
//}
