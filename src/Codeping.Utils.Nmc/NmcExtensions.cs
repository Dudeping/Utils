using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Codeping.Utils.Nmc
{
    /// <summary>
    /// 系统扩展 - 获取天气
    /// </summary>
    public static class NmcExtensions
    {
        /// <summary>
        /// 获取天气预报
        /// 通过标签标识预报的唯一性
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="isMobile">是否返回移动端链接</param>
        /// <returns></returns>
        public static async Task<Result<ForecastModel>> RequestForecastAsync(
            [NotNull]this HttpClient client, ForecastType type = ForecastType.天气公报, bool isMobile = true)
        {
            var result = new Result<ForecastModel>();

            try
            {
                var address = GetForecastAddress(type);

                var domain = GetDomain(isMobile);

                var url = domain + address;

                string html = await client.GetStringAsync(url);

                string text = Regex.Match(html, "<div\\s*class=\"author\">([\\s\\S]*?)</div>").Groups?[1]?.Value;

                MatchCollection numbers = Regex.Matches(text, @"<b>(\d*)</b>");

                string datetime = numbers.ToList(x => x.Groups?[1]?.Value ?? string.Empty).Join("/");

                var model = new ForecastModel()
                {
                    Type = type,
                    Label = datetime,
                    DateTime = DateTime.Now,
                    Url = url,
                };

                return result.Ok(model);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        /// <summary>
        /// 获取城市天气预报
        /// </summary>
        /// <param name="client">客户端</param>
        /// <returns></returns>
        public static async Task<Result<CityForecast>> RequestCityForecastAsync([NotNull]this HttpClient client)
        {
            var result = new Result<CityForecast>();

            try
            {
                string html = await client.GetStringAsync(Constants.FORECAST_CHINA);

                string title = Regex.Match(html, "<h2 class=\"fl\">([^<]*)</h2>").Groups?[1]?.Value;

                string[] areas = html.Split(new[] { "<div class=\"area\" style=\"display:none;\">" }, StringSplitOptions.RemoveEmptyEntries);

                string area = areas[6];

                MatchCollection lis = Regex.Matches(area, "<li[^>]*>(.*?)</li>", RegexOptions.Singleline);

                var forecast = new CityForecast() { Title = title };

                foreach (Match li in lis)
                {
                    string text = li.Groups?[1]?.Value;

                    Match url_cname = Regex.Match(text, "<a target=\"_blank\" href=\"([^\"]*)\">([^<]*)</a>");

                    string url = url_cname.Groups?[1]?.Value;

                    string cname = url_cname.Groups?[2]?.Value;

                    string weather = Regex.Match(text, "<div class=\"weather\">([^<]*)</div>").Groups?[1]?.Value;

                    string temp = Regex.Match(text, "<div class=\"temp\">([^<]*)</div>").Groups?[1]?.Value;

                    forecast.Forecats.Add(new ForecastItem
                    {
                        CName = cname,
                        Weather = weather,
                        Temp = temp,
                        Url = NmcExtensions.GetDomain(false) + url,
                        MobileUrl = NmcExtensions.GetDomain(true) + url,
                    });
                }

                return result.Ok(forecast);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        /// <summary>
        /// 获取当日预警信息
        /// </summary>
        /// <param name="client">客户端</param>
        /// <returns></returns>
        public static async Task<Result<AlarmResult>> RequestAlarmAsync(
            [NotNull]this HttpClient client, AlarmType type, AlarmLevel level, AlarmArea area, int page = 1, bool isMobile = true)
        {
            var result = new Result<AlarmResult>();

            try
            {
                var signaltype = type == AlarmType.全部类型 ? "" : HttpUtility.UrlEncode(type.ToString(), Encoding.UTF8);
                var signallevel = level == AlarmLevel.全部等级 ? "" : HttpUtility.UrlEncode(level.ToString(), Encoding.UTF8);
                var province = area == AlarmArea.全部区域 ? "" : HttpUtility.UrlEncode(area.ToString(), Encoding.UTF8);

                string text = $"pageNo={page}&pageSize=30&signaltype={signaltype}&signallevel={signallevel}&province={province}";

                var content = new StringContent(text, Encoding.UTF8, "application/x-www-form-urlencoded");

                var url = Constants.DOMAIN + Constants.FORECAST_ALARM;

                HttpResponseMessage response = await client.PostAsync(url, content);

                string html = await response.Content.ReadAsStringAsync();

                MatchCollection evens = Regex.Matches(html, "<div class=\"(?:even|odd)\">(.*?)</div>", RegexOptions.Singleline);

                var items = NmcExtensions.HandeAlarmAsync(client, evens, isMobile);

                var total = Regex.Match(html, "条，共 (\\d*) 条").Groups[1].Value;

                var model = new AlarmResult { Items = items, Total = int.Parse(total) };

                return result.Ok(model);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        private static async IAsyncEnumerable<AlarmModel> HandeAlarmAsync(HttpClient client, MatchCollection matches, bool isMobile = true)
        {
            foreach (Match odd in matches)
            {
                string text = odd.Groups?[1]?.Value;

                Match url_text = Regex.Match(text, "<a target=\"_blank\" href=\"([^\"]*)\">(.*?)</a>", RegexOptions.Singleline);

                string url = url_text.Groups?[1]?.Value;

                string words = url_text.Groups?[2]?.Value;

                if (string.IsNullOrWhiteSpace(url) ||
                    string.IsNullOrWhiteSpace(url))
                {
                    continue;
                }

                url = NmcExtensions.GetDomain(isMobile) + url;

                var page = await client.GetStringAsync(url);

                if (page.Contains("很抱歉，您要访问的页面地址已经失效或者页面不存在."))
                {
                    continue;
                }

                words = Regex.Replace(HttpUtility.HtmlDecode(words.Replace("&nbsp;", "")), "<[^>]*>", "");

                string datetime = Regex.Match(text, "<span class=\"date\">([^<]*)</span>").Groups?[1]?.Value;

                string date = datetime.Replace("年", "-").Replace("月", "-");

                int index = date.IndexOf("日");

                if (index > -1)
                {
                    date = date.Remove(index);
                }

                var odate = DateTime.Parse(date);

                if (odate.Date != DateTime.Now.Date)
                {
                    continue;
                }

                yield return new AlarmModel
                {
                    Label = words,
                    DateTime = odate,
                    Url = url,
                };
            }
        }

        private static string GetForecastAddress(ForecastType type)
        {
            switch (type)
            {
                case ForecastType.天气公报: return Constants.FORECAST_WEATHER_BULLETIN;
                case ForecastType.暴雨预警: return Constants.FORECAST_WARNING_DOWNPOUR;
                case ForecastType.大雾预警: return Constants.FORECAST_WARNING_FOG;
                case ForecastType.每日天气提示: return Constants.FORECAST_WEATHER_PERDAY;
                case ForecastType.中期天气预报: return Constants.FORECAST_MIDRANGE_BULLETIN;
                case ForecastType.国外天气预报: return Constants.FORECAST_ABROAD_WEATHER;
                case ForecastType.环境气象公报: return Constants.FORECAST_ENVIRONMENTAL;
                case ForecastType.公路气象预报: return Constants.FORECAST_TRAFFIC;
                case ForecastType.强对流天气预报: return Constants.FORECAST_SWPC_BULLETIN;
                case ForecastType.森林火险气象预报: return Constants.FORECAST_FORESTFIRE_DOC;
                case ForecastType.草原火险气象预报: return Constants.FORECAST_GLASSLAND_FIRE;
                default: throw new NotSupportedException(type.ToString());
            }
        }

        private static string GetDomain(bool isMobile)
        {
            return isMobile ? Constants.M_DOMAIN : Constants.DOMAIN;
        }
    }
}
