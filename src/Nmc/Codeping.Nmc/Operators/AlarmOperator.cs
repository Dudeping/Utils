using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Codeping.Utils;

namespace Codeping.Nmc
{
    internal class AlarmOperator
    {
        private const string TEXT_NOTFOUND = "很抱歉，您要访问的页面地址已经失效或者页面不存在.";

        private static readonly Regex _events = new Regex(
            "<div class=\"(?:even|odd)\">(.*?)</div>", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex _url = new Regex(
            "<a target=\"_blank\" href=\"([^\"]*)\">(.*?)</a>", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex _datetime = new Regex(
            "<span class=\"date\">([^<]*)</span>", RegexOptions.Compiled);
        private static readonly Regex _mark = new Regex("<[^>]*>", RegexOptions.Compiled);

        private readonly NmcClient _client;

        public AlarmOperator(NmcClient client)
        {
            _client = client;
        }

        public async Task<Result<IList<AlarmModel>>> RequestAsync(
            AlarmType type = AlarmType.全部类型,
            AlarmLevel level = AlarmLevel.全部等级,
            AlarmArea area = AlarmArea.全部区域,
            int page = 1,
            bool isMobile = true)
        {
            var result = new Result<IList<AlarmModel>>();

            try
            {
                var signaltype = type == AlarmType.全部类型 ? "" : HttpUtility.UrlEncode(type.ToString(), Encoding.UTF8);
                var signallevel = level == AlarmLevel.全部等级 ? "" : HttpUtility.UrlEncode(level.ToString(), Encoding.UTF8);
                var province = area == AlarmArea.全部区域 ? "" : HttpUtility.UrlEncode(area.ToString(), Encoding.UTF8);

                var text = $"pageNo={page}&pageSize=30&signaltype={signaltype}&signallevel={signallevel}&province={province}";

                var content = new StringContent(text, Encoding.UTF8, "application/x-www-form-urlencoded");

                var url = Constants.DOMAIN + Constants.FORECAST_ALARM;

                var response = await _client.PostAsync(url, content).ConfigureAwait(false);

                if (!response.Succeeded)
                {
                    return result.Merge(response);
                }

                var evens = _events.Matches(response.Value);

                var items = await this.HandeAlarmAsync(evens, isMobile);

                return result.Ok(items);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        private async Task<IList<AlarmModel>> HandeAlarmAsync(MatchCollection matches, bool isMobile = true)
        {
            IList<AlarmModel> alarms = new List<AlarmModel>();

            foreach (Match odd in matches)
            {
                var text = odd.Groups?[1]?.Value;

                var url_text = _url.Match(text);

                var url = url_text.Groups?[1]?.Value;

                var words = url_text.Groups?[2]?.Value;

                if (String.IsNullOrWhiteSpace(url) || String.IsNullOrWhiteSpace(url))
                {
                    continue;
                }

                words = _mark.Replace(HttpUtility.HtmlDecode(words.Replace("&nbsp;", "")), "");

                var datetime = _datetime.Match(text).Groups?[1]?.Value;

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

                url = _client.GetDomain(isMobile) + url;

                var content = await _client.GetAsync(url).ConfigureAwait(false);

                if (!content.Succeeded || content.Value.Contains(TEXT_NOTFOUND))
                {
                    continue;
                }

                var model = new AlarmModel
                {
                    Url = url,
                    Label = words,
                    DateTime = odate,
                };

                alarms.Add(model);
            }

            return alarms;
        }
    }
}
