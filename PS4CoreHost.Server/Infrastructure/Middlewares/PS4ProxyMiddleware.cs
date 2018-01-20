using Microsoft.AspNetCore.Builder;
using PS4CoreHost.Utils;
using System.Threading.Tasks;

namespace PS4CoreHost.Server.Infrastructure.Middlewares
{
    internal static class PS4ProxyExtensions
    {
        public static IApplicationBuilder UsePS4Proxy(
            this IApplicationBuilder app)
        {
            app.MapWhen(ctx => ctx.Request.IsUserManuals(), builder => RedirectHandler(builder, "/entry"));
            return app;
        }

        private static void RedirectHandler(IApplicationBuilder app, string redirectUrl)
        {
            app.Run(ctx =>
            {
                ctx.Response.Redirect(redirectUrl);
                return Task.CompletedTask;
            });
        }
    }
}
