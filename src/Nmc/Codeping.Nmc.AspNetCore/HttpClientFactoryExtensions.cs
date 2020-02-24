using System.Net.Http;

namespace Codeping.Nmc.AspNetCore
{
    public static class HttpClientFactoryExtensions
    {
        public static NmcClient GetNmcClient(this IHttpClientFactory factory)
        {
            var client = factory.CreateClient(NmcName.Value);

            return NmcClient.Create(client);
        }
    }
}
