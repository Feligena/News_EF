using HW_EF.Middlewares.Services;
using Microsoft.AspNetCore.Identity;

namespace HW_EF.Middlewares
{
    public class KeyMiddleware
    {
        private RequestDelegate next;

        public KeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var userManager = context.RequestServices.GetRequiredService<IUserManager>();

            var userCredentials = userManager.GetCredentials();
            if (userCredentials != null)
            {
                await next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("Unauthorized");
            }



        }
    }
}
