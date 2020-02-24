using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Codeping.Utils;

namespace Codeping.Nmc
{
    internal class CityForecastOperator
    {
        private readonly static Regex _title = new Regex(
            "<h2 class=\"fl\">([^<]*)</h2>", RegexOptions.Compiled);
        private readonly static Regex _url_Cname = new Regex(
            "<a target=\"_blank\" href=\"([^\"]*)\">([^<]*)</a>", RegexOptions.Compiled);
        private readonly static Regex _weather = new Regex(
            "<div class=\"weather\">([^<]*)</div>", RegexOptions.Compiled);
        private readonly static Regex _temp = new Regex(
            "<div class=\"temp\">([^<]*)</div>", RegexOptions.Compiled);


        private readonly NmcClient _client;

        public CityForecastOperator(NmcClient client)
        {
            _client = client;
        }

        public async Task<Result<IDictionary<CityAreaType, CityForecast>>> RequestAsync()
        {
            var result = new Result<IDictionary<CityAreaType, CityForecast>>();

            var forecasts = new Dictionary<CityAreaType, CityForecast>();

            try
            {
                var response = await _client
                    .GetAsync(Constants.DOMAIN + Constants.FORECAST_CHINA)
                    .ConfigureAwait(false);

                if (!response.Succeeded)
                {
                    return result.Merge(response);
                }

                var title = _title.Match(response.Value).Groups[1].Value;

                var separator = new[]
                {
                    "<div class=\"area\">",
                    "<div class=\"area\" style=\"display:none;\">",
                };

                var areas = response.Value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 1; i < 9; i++)
                {
                    var area = areas[i];

                    var lis = Regex.Matches(area, "<li[^>]*>(.*?)</li>", RegexOptions.Singleline);

                    var forecast = new CityForecast() { Title = title };

                    foreach (Match li in lis)
                    {
                        var text = li.Groups?[1]?.Value;

                        var url_cname = _url_Cname.Match(text);

                        var url = url_cname.Groups[1].Value;

                        var cname = url_cname.Groups[2].Value;

                        var weather = _weather.Match(text).Groups[1].Value;

                        var temp = _temp.Match(text).Groups[1].Value;

                        forecast.Forecats.Add(new ForecastItem
                        {
                            CName = cname?.Trim(),
                            Weather = weather?.Trim(),
                            Temp = temp?.Trim(),
                            Url = _client.GetDomain(false) + url,
                            MobileUrl = _client.GetDomain(true) + url,
                        });
                    }

                    forecasts[(CityAreaType)i - 1] = forecast;
                }

                return result.Ok(forecasts);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

    }
}
