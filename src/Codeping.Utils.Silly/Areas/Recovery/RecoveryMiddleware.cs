using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
            if (IsEnable)
            {
                httpContext.Response.Redirect("/Recovery/Index");
            }

            return _next(httpContext);
        }
    }

}
