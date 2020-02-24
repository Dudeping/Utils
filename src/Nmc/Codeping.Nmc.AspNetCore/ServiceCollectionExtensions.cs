using Codeping.Nmc;
using Codeping.Nmc.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNmc(this IServiceCollection services)
        {
            services.AddHttpClient<NmcClient>(NmcName.Value)
                .ConfigurePrimaryHttpMessageHandler(provider => NmcClient.CreateHttpHandler());

            return services;
        }
    }
}
