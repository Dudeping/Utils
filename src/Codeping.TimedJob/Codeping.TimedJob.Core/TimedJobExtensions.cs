using Codeping.TimedJob.Core;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TimedJobExtensions
    {
        public static IServiceCollection AddTimedJob(this IServiceCollection services)
        {
            services.AddHostedService<TimedJobService>();

            return services;
        }

        public static IServiceCollection AddTimedJob(this IServiceCollection services, Action<TimedJobOptions> setupAction)
        {
            services.AddTimedJob();

            if (setupAction != null)
            {
                services.AddScoped(x =>
                {
                    var options = new TimedJobOptions();

                    setupAction(options);

                    return options;
                });
            }

            return services;
        }

        public static TimedJobOptions UseDb<TDbContext>(this TimedJobOptions options)
            where TDbContext : DbContext
        {
            options.DbContext = typeof(TDbContext);

            return options;
        }
    }
}