using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Codeping.Nmc.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Codeping.Nmc.Test
{
    public class NmcTest_AspNetCore
    {
        private readonly NmcClient _client;

        public NmcTest_AspNetCore()
        {
            _client = new ServiceCollection()
                .AddNmc()
                .BuildServiceProvider()
                .GetService<IHttpClientFactory>()
                .GetNmcClient();
        }

        [Theory]
        [InlineData(ForecastType.天气公报, true)]
        [InlineData(ForecastType.天气公报, false)]

        [InlineData(ForecastType.中期天气预报, true)]
        [InlineData(ForecastType.中期天气预报, false)]

        [InlineData(ForecastType.公路气象预报, true)]
        [InlineData(ForecastType.公路气象预报, false)]

        [InlineData(ForecastType.国外天气预报, true)]
        [InlineData(ForecastType.国外天气预报, false)]

        [InlineData(ForecastType.大雾预警, true)]
        [InlineData(ForecastType.大雾预警, false)]

        [InlineData(ForecastType.强对流天气预报, true)]
        [InlineData(ForecastType.强对流天气预报, false)]

        [InlineData(ForecastType.暴雨预警, true)]
        [InlineData(ForecastType.暴雨预警, false)]

        [InlineData(ForecastType.森林火险气象预报, true)]
        [InlineData(ForecastType.森林火险气象预报, false)]

        [InlineData(ForecastType.每日天气提示, true)]
        [InlineData(ForecastType.每日天气提示, false)]

        [InlineData(ForecastType.环境气象公报, true)]
        [InlineData(ForecastType.环境气象公报, false)]

        [InlineData(ForecastType.草原火险气象预报, true)]
        [InlineData(ForecastType.草原火险气象预报, false)]
        public async Task ForeactTestAsync(ForecastType type, bool isMobile)
        {
            var result = await _client
                .RequestForecastAsync(type, isMobile)
                .ConfigureAwait(false);

            Assert.True(result.Succeeded);

            Assert.NotNull(result.Value.Label);
            Assert.Equal(type, result.Value.Type);

            var status = await _client.HttpClient.GetAsync(result.Value.Url);

            Assert.Equal(HttpStatusCode.OK, status.StatusCode);
        }

        [Fact]
        public async Task CityForeactTestAsync()
        {
            var result = await _client
                .RequestCityForecastAsync()
                .ConfigureAwait(false);

            Assert.True(result.Succeeded);

            Assert.NotEmpty(result.Value);

            foreach (var item in result.Value.Values)
            {
                Assert.NotEmpty(item.Forecats);
            }
        }

        [Fact]
        public async Task AlarmTestAsync()
        {
            var result = await _client
                .RequestAlarmAsync()
                .ConfigureAwait(false);

            Assert.True(result.Succeeded);

            Assert.NotEmpty(result.Value);
        }
    }
}
