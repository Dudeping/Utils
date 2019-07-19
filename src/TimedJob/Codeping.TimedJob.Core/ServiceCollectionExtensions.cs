using Codeping.TimedJob.Core;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TimedJobServiceCollectionExtensions
    {
        public static IServiceCollection AddTimedJob(this IServiceCollection services)
        {
            services.AddHostedService<TimedJobService>();

            return services;
        }
    }
}