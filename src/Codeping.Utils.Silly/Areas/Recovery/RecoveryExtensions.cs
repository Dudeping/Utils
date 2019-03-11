using Codeping.Utils.Silly.Areas.Recovery;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RecoveryExtensions
    {
        public static IApplicationBuilder UseEnvironmentCore(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RecoveryMiddleware>();
        }
    }
}
