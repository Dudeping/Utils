using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Codeping.Utils.Silly.Areas.Recovery
{
    public class RecoveryMiddleware
    {
        public static bool IsEnable = false;

        private readonly RequestDelegate _next;

        public RecoveryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (IsEnable && httpContext.Request.Path != "/Recovery/Index")
            {
                httpContext.Response.Redirect("/Recovery/Index");

                return Task.CompletedTask;
            }
            else
            {
                return _next(httpContext);
            }
        }
    }

}
