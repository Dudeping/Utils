using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Codeping.Utils;

[assembly: InternalsVisibleTo("Codeping.Nmc.AspNetCore")]
namespace Codeping.Nmc
{
    public class NmcClient
    {
        private readonly AlarmOperator _alarm;
        private readonly ForecastOperator _forecast;
        private readonly CityForecastOperator _cityForecast;

        private NmcClient(HttpClient httpClient)
        {
            this.HttpClient = httpClient;

            _alarm = new AlarmOperator(this);
            _forecast = new ForecastOperator(this);
            _cityForecast = new CityForecastOperator(this);
        }

        public HttpClient HttpClient { get; }

        /// <summary>
        /// 获取当日预警信息
        /// </summary>
        /// <param name="type">预警类型</param>
        /// <param name="level">预警级别</param>
        /// <param name="area">预警区域</param>
        /// <param name="page">页面</param>
        /// <param name="isMobile">是否返回移动端链接</param>
        /// <returns></returns>
        public async Task<Result<IList<AlarmModel>>> RequestAlarmAsync(
            AlarmType type = AlarmType.全部类型,
            AlarmLevel level = AlarmLevel.全部等级,
            AlarmArea area = AlarmArea.全部区域,
            int page = 1,
            bool isMobile = true)
        {
            return await _alarm.RequestAsync(type, level, area, page, isMobile).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取城市天气预报
        /// </summary>
        /// <param name="client">客户端</param>
        /// <returns></returns>
        public async Task<Result<IDictionary<CityAreaType, CityForecast>>> RequestCityForecastAsync()
        {
            return await _cityForecast.RequestAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 获取天气预报
        /// 通过标签标识预报的唯一性
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="type">天气预报类型</param>
        /// <param name="isMobile">是否返回移动端链接</param>
        /// <returns></returns>
        public async Task<Result<ForecastModel>> RequestForecastAsync(
            ForecastType type = ForecastType.天气公报, bool isMobile = true)
        {
            return await _forecast.RequestAsync(type, isMobile);
        }

        internal async Task<Result<string>> GetAsync(string url)
        {
            return await this.NmcSendAsync(this.HttpClient.GetAsync(url)).ConfigureAwait(false);
        }

        internal async Task<Result<string>> PostAsync(string url, HttpContent content)
        {
            return await this.NmcSendAsync(this.HttpClient.PostAsync(url, content)).ConfigureAwait(false);
        }

        private async Task<Result<string>> NmcSendAsync(Task<HttpResponseMessage> responseTask)
        {
            var result = new Result<string>();

            try
            {
                var response = await responseTask;

                if (!response.IsSuccessStatusCode)
                {
                    return result.Fail(response.StatusCode.ToString());
                }

                using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                using var gzip = new GZipStream(stream, CompressionMode.Decompress);

                using var reader = new StreamReader(gzip);

                var html = await reader.ReadToEndAsync().ConfigureAwait(false);

                return result.Ok(html);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        internal string GetDomain(bool isMobile)
        {
            return isMobile ? Constants.M_DOMAIN : Constants.DOMAIN;
        }

        public static NmcClient Create()
        {
            return Create(new HttpClient(CreateHttpHandler()));
        }

        internal static NmcClient Create(HttpClient client)
        {
            ConfigurationHttpClient(client);

            return new NmcClient(client);
        }

        internal static HttpMessageHandler CreateHttpHandler()
        {
            var handler = new HttpClientHandler();

            handler.UseCookies = true;

            handler.UseProxy = true;

            handler.Proxy = WebRequest.GetSystemWebProxy();

            handler.AllowAutoRedirect = true;

            handler.ServerCertificateCustomValidationCallback += (a, b, c, d) => true;

            return handler;
        }

        internal static HttpClient ConfigurationHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.TryParseAdd(
                "text/html," +
                "application/xhtml+xml," +
                "application/xml;q=0.9," +
                "image/webp," +
                "image/apng," +
                "*/*;q=0.8," +
                "application/signed-exchange;v=b3;q=0.9");

            client.DefaultRequestHeaders.UserAgent.TryParseAdd(
                "Mozilla/5.0 " +
                "(Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 " +
                "(KHTML, like Gecko) " +
                "Chrome/79.0.3945.130 Safari/537.36");

            client.DefaultRequestHeaders.AcceptEncoding.TryParseAdd("gzip");

            client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("zh-CN,zh;q=0.9");

            client.DefaultRequestHeaders.Connection.TryParseAdd("keep-alive");

            client.DefaultRequestHeaders.Pragma.TryParseAdd("no-cache");

            return client;
        }
    }
}
