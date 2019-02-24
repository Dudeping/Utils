using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codeping.Utils.TimedJob;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TimedJobExtensions
    {
        public static IServiceCollection AddTimedJob(this IServiceCollection services)
        {
            return services.AddSingleton<IAssemblyLocator, DefaultAssemblyLocator>()
                .AddSingleton<TimedJobService>();
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class TimedJobExtensions
    {
        public static IApplicationBuilder UseTimedJob(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.GetRequiredService<TimedJobService>();

            return builder;
        }
    }
}
