using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Utils.Tests
{
    public class ExtNmcTest
    {
        [Fact]
        public async Task RequestBulletinTestAsync()
        {
            var client = new HttpClient();

            var result = await client.RequestBulletinAsync();

            Assert.True(result.Succeeded);

            Assert.NotNull(result.Value);

            Assert.Contains("m.nmc.cn", result.Value.Url);

            result = await client.RequestBulletinAsync(false);

            Assert.True(result.Succeeded);

            Assert.NotNull(result.Value);

            Assert.Contains("www.nmc.cn", result.Value.Url);

            client = null;

            result = await client.RequestBulletinAsync();

            Assert.False(result.Succeeded);
        }

        [Theory]
        [InlineData("四川省")]
        [InlineData("云南省")]
        [InlineData("广东省")]
        public async Task RequestAlarmTestAsync(string data)
        {
            var client = new HttpClient();

            var result = await client.RequestAlarmAsync(data);

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);

            if (result.Value.Any())
            {
                Assert.True(result.Value.All(x => x.Label.Contains(data) && x.Url.Contains("m.nmc.cn")));
            }

            result = await client.RequestAlarmAsync(data, false);

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);

            if (result.Value.Any())
            {
                Assert.True(result.Value.All(x => x.Label.Contains(data) && x.Url.Contains("www.nmc.cn")));
            }

            client = null;

            result = await client.RequestAlarmAsync(data);

            Assert.False(result.Succeeded);
        }

        [Fact]
        public async Task RequestForecastAsync()
        {
            var client = new HttpClient();

            var result = await client.RequestForecastAsync("m.a.cn");

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.Contains("m.a.cn", result.Value.Url);
            Assert.Contains("m.nmc.cn", result.Value.Label);

            result = await client.RequestForecastAsync("m.a.cn", false);

            Assert.True(result.Succeeded);
            Assert.NotNull(result.Value);
            Assert.Contains("m.a.cn", result.Value.Url);
            Assert.Contains("www.nmc.cn", result.Value.Label);

            client = null;

            result = await client.RequestForecastAsync("m.a.cn");

            Assert.False(result.Succeeded);
        }
    }
}
