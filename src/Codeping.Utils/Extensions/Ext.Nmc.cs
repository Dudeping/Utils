using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 获取天气
    /// </summary>
    public static partial class Ext
    {
        private static readonly string _alarm = "http://www.nmc.cn/f/alarm.html";
        private static readonly string _gulletin = "http://www.nmc.cn/publish/weather-bulletin/index.htm";
        private static readonly string _forecast = "http://www.nmc.cn/publish/forecast/china.html";

        /// <summary>
        /// 获取天气公报
        /// </summary>
        /// <param name="client">客户端</param>
        /// <returns></returns>
        public static NmcModel RequestBulletin(this HttpClient client)
        {
            try
            {
                var html = client.GetStringAsync(_gulletin).Result;

                var text = Regex.Match(html, "<div class=\"author\">([^=]*)</div>").Groups?[1]?.Value;

                var numbers = Regex.Matches(text, @"<b>(\d*)</b>");

                var datetime = string.Join("/", numbers.ToList<Match>().Select(x => x.Groups?[1]?.Value ?? string.Empty));

                return new NmcModel()
                {
                    Label = datetime,
                    DateTime = DateTime.Now,
                    Url = "http://m.nmc.cn/publish/weather-bulletin/index.htm",
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取预警信息
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="region">省份, 如: 四川省</param>
        public static IEnumerable<NmcModel> RequestAlarm(this HttpClient client, string region)
        {
            var url = "pageNo=1&pageSize=30&signaltype=&signallevel=&province=" + HttpUtility.UrlEncode(region, Encoding.UTF8);

            var content = new StringContent(url, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = client.PostAsync(_alarm, content).Result;

            var html = response.Content.ReadAsStringAsync().Result;

            var evens = Regex.Matches(html, "<div class=\"even\">(.*?)</div>", RegexOptions.Singleline);

            var odds = Regex.Matches(html, "<div class=\"odd\">(.*?)</div>", RegexOptions.Singleline);

            return Ext.HandeAlarm(evens).Union(Ext.HandeAlarm(odds));
        }

        /// <summary>
        /// 获取城市天气预报
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="domain">当前域</param>
        /// <returns></returns>
        public static IEnumerable<NmcModel> RequestForecast(this HttpClient client, string domain)
        {
            var html = client.GetStringAsync(_forecast).Result;

            var title = Regex.Match(html, "<h2 class=\"fl\">([^<]*)</h2>").Groups?[1]?.Value;

            var areas = html.Split(new[] { "<div class=\"area\" style=\"display:none;\">" }, StringSplitOptions.RemoveEmptyEntries);

            var area = areas[6];

            var lis = Regex.Matches(area, "<li>(.*?)</li>", RegexOptions.Singleline);

            var forecast = new Forecast() { Title = title };

            foreach (Match li in lis)
            {
                var text = li.Groups?[1]?.Value;

                var url_cname = Regex.Match(text, "<a target=\"_blank\" href=\"([^\"]*)\">([^<]*)</a>");

                var url = url_cname.Groups?[1]?.Value;

                var cname = url_cname.Groups?[2]?.Value;

                var weather = Regex.Match(text, "<div class=\"weather\">([^<]*)</div>").Groups?[1]?.Value;

                var temp = Regex.Match(text, "<div class=\"temp\">([^<]*)</div>").Groups?[1]?.Value;

                forecast.Forecats.Add(new ForecastItem
                {
                    CName = cname,
                    Weather = weather,
                    Temp = temp,
                    Url = "http://m.nmc.cn" + url,
                });
            }

            var label = forecast.ToJson();

            yield return new NmcModel
            {
                Label = label,
                DateTime = DateTime.Now,
                Url = $"{domain}/weather/forecast",
            };
        }

        private static IEnumerable<NmcModel> HandeAlarm(MatchCollection matches)
        {
            foreach (Match odd in matches)
            {
                var text = odd.Groups?[1]?.Value;

                var url_text = Regex.Match(text, "<a target=\"_blank\" href=\"([^\"]*)\">(.*?)</a>", RegexOptions.Singleline);

                var url = url_text.Groups?[1]?.Value;
                var words = url_text.Groups?[2]?.Value;

                words = Regex.Replace(HttpUtility.HtmlDecode(words.Replace("&nbsp;", "")), "<[^>]*>", "");

                var datetime = Regex.Match(text, "<span class=\"date\">([^<]*)</span>").Groups?[1]?.Value;

                var date = datetime.Replace("年", "-").Replace("月", "-");

                var index = date.IndexOf("日");

                if (index > -1)
                {
                    date = date.Remove(index);
                }

                var odate = DateTime.Parse(date);

                if (odate.Date != DateTime.Now.Date)
                {
                    continue;
                }

                url = "http://m.nmc.cn" + url;

                yield return new NmcModel
                {
                    Label = words,
                    DateTime = odate,
                    Url = url,
                };
            }
        }

        /// <summary>
        /// 返回类型
        /// </summary>
        public class NmcModel
        {
            /// <summary>
            /// 抓取标记
            /// </summary>
            public string Label { get; set; }
            /// <summary>
            /// 抓取链接
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// 抓取日期
            /// </summary>
            public DateTime DateTime { get; set; }
        }

        /// <summary>
        /// 城市天气
        /// </summary>
        public class Forecast
        {
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            public List<ForecastItem> Forecats { get; set; } = new List<ForecastItem>();
        }

        /// <summary>
        /// 城市天气内容项
        /// </summary>
        public class ForecastItem
        {
            /// <summary>
            /// 城市名
            /// </summary>
            public string CName { get; set; }

            /// <summary>
            /// 天气页
            /// </summary>
            public string Url { get; set; }

            /// <summary>
            /// 天气
            /// </summary>
            public string Weather { get; set; }

            /// <summary>
            /// 温度
            /// </summary>
            public string Temp { get; set; }
        }
    }
}
