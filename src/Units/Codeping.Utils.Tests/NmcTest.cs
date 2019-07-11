using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;
using Codeping.Utils.Nmc;
using System.Threading.Tasks;
using System.Net;

namespace Codeping.Utils.Tests
{
    public class NmcTest
    {
        private readonly HttpClient _client;

        public NmcTest()
        {
            _client = new HttpClient();
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
            var result = await _client.RequestForecastAsync(type, isMobile);

            Assert.True(result.Succeeded);

            Assert.NotNull(result.Value.Label);
            Assert.Equal(type, result.Value.Type);

            var status = await _client.GetAsync(result.Value.Url);

            Assert.Equal(HttpStatusCode.OK, status.StatusCode);
        }

        [Fact]
        public async Task CityForeactTestAsync()
        {
            var result = await _client.RequestCityForecastAsync();

            Assert.True(result.Succeeded);

            Assert.NotEmpty(result.Value.Title);
            Assert.NotEmpty(result.Value.Forecats);
        }

        [Fact]
        public async Task AlarmTestAsync()
        {
            var result = await _client.RequestAlarmAsync();

            Assert.True(result.Succeeded);

            Assert.True(result.Value.Total > 0);
        }
    }
}
