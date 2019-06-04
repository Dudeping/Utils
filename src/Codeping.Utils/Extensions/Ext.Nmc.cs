using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 获取天气
    /// </summary>
    public static partial class Ext
    {
        private const string _alarm = "http://www.nmc.cn/f/alarm.html";
        private const string _gulletin = "http://www.nmc.cn/publish/weather-bulletin/index.htm";
        private const string _forecast = "http://www.nmc.cn/publish/forecast/china.html";

        private const string _mgulletin = "http://m.nmc.cn/publish/weather-bulletin/index.htm";
        private static string GetDomain(bool isMobile = true) => isMobile ? "http://m.nmc.cn" : "http://www.nmc.cn";

        /// <summary>
        /// 获取天气公报
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="isMobile">是否响应移动端链接</param>
        /// <returns></returns>
        public static async Task<Result<NmcModel>> RequestBulletinAsync(
            [NotNull]this HttpClient client, bool isMobile = true)
        {
            var result = new Result<NmcModel>();

            try
            {
                string html = await client.GetStringAsync(_gulletin);

                string text = Regex.Match(html, "<div class=\"author\">([^=]*)</div>").Groups?[1]?.Value;

                MatchCollection numbers = Regex.Matches(text, @"<b>(\d*)</b>");

                string datetime = numbers.ToList(x => x.Groups?[1]?.Value ?? string.Empty).Join("/");

                var model = new NmcModel()
                {
                    Label = datetime,
                    DateTime = DateTime.Now,
                    Url = isMobile ? _mgulletin : _gulletin,
                };

                return result.Ok(model);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        /// <summary>
        /// 获取预警信息
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="region">省份, 如: 四川省</param>
        /// <param name="isMobile">是否响应移动端链接</param>
        /// <returns></returns>
        public static async Task<Result<IEnumerable<NmcModel>>> RequestAlarmAsync(
            [NotNull]this HttpClient client, string region, bool isMobile = true)
        {
            var result = new Result<IEnumerable<NmcModel>>();

            try
            {
                string url = "pageNo=1&pageSize=30&signaltype=&signallevel=&province=" + HttpUtility.UrlEncode(region, Encoding.UTF8);

                StringContent content = new StringContent(url, Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.PostAsync(_alarm, content);

                string html = response.Content.ReadAsStringAsync().Result;

                MatchCollection evens = Regex.Matches(html, "<div class=\"even\">(.*?)</div>", RegexOptions.Singleline);

                MatchCollection odds = Regex.Matches(html, "<div class=\"odd\">(.*?)</div>", RegexOptions.Singleline);

                var items = Ext.HandeAlarm(evens, isMobile).Union(Ext.HandeAlarm(odds, isMobile));

                return result.Ok(items);
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
        /// <param name="domain">当前域</param>
        /// <param name="isMobile">是否响应移动端链接</param>
        /// <returns></returns>
        public static async Task<Result<NmcModel>> RequestForecastAsync(
            [NotNull]this HttpClient client, string domain, bool isMobile = true)
        {
            var result = new Result<NmcModel>();

            try
            {
                string html = await client.GetStringAsync(_forecast);

                string title = Regex.Match(html, "<h2 class=\"fl\">([^<]*)</h2>").Groups?[1]?.Value;

                string[] areas = html.Split(new[] { "<div class=\"area\" style=\"display:none;\">" }, StringSplitOptions.RemoveEmptyEntries);

                string area = areas[6];

                MatchCollection lis = Regex.Matches(area, "<li[^>]*>(.*?)</li>", RegexOptions.Singleline);

                Forecast forecast = new Forecast() { Title = title };

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
                        Url = Ext.GetDomain(isMobile) + url,
                    });
                }

                string label = forecast.ToJson();

                var model = new NmcModel
                {
                    Label = label,
                    DateTime = DateTime.Now,
                    Url = $"{domain}/weather/forecast",
                };

                return result.Ok(model);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        private static IEnumerable<NmcModel> HandeAlarm(MatchCollection matches, bool isMobile = true)
        {
            foreach (Match odd in matches)
            {
                string text = odd.Groups?[1]?.Value;

                Match url_text = Regex.Match(text, "<a target=\"_blank\" href=\"([^\"]*)\">(.*?)</a>", RegexOptions.Singleline);

                string url = url_text.Groups?[1]?.Value;
                string words = url_text.Groups?[2]?.Value;

                words = Regex.Replace(HttpUtility.HtmlDecode(words.Replace("&nbsp;", "")), "<[^>]*>", "");

                string datetime = Regex.Match(text, "<span class=\"date\">([^<]*)</span>").Groups?[1]?.Value;

                string date = datetime.Replace("年", "-").Replace("月", "-");

                int index = date.IndexOf("日");

                if (index > -1)
                {
                    date = date.Remove(index);
                }

                DateTime odate = DateTime.Parse(date);

                if (odate.Date != DateTime.Now.Date)
                {
                    continue;
                }

                yield return new NmcModel
                {
                    Label = words,
                    DateTime = odate,
                    Url = Ext.GetDomain(isMobile) + url,
                };
            }
        }
    }
}
