using Codeping.Utils.TimedJob;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TimedJobExtensions
    {
        public static IServiceCollection AddTimedJob(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAssemblyLocator, AssemblyLocator>()
                .AddHostedService<TimedJobService>();
        }
    }
}