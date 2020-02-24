using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Codeping.Utils;

namespace Codeping.Nmc
{
    internal class ForecastOperator
    {
        private readonly static Regex _text = new Regex(
            "<div\\s*class=\"author\">([\\s\\S]*?)</div>", RegexOptions.Compiled);
        private readonly static Regex _number = new Regex(@"<b>(\d*)</b>", RegexOptions.Compiled);

        private readonly NmcClient _client;

        public ForecastOperator(NmcClient client)
        {
            _client = client;
        }

        public async Task<Result<ForecastModel>> RequestAsync(
            ForecastType type = ForecastType.天气公报, bool isMobile = true)
        {
            var result = new Result<ForecastModel>();

            try
            {
                var address = type.ToAddress();

                var domain = _client.GetDomain(isMobile);

                var url = domain + address;

                var response = await _client.GetAsync(url).ConfigureAwait(false);

                if (!response.Succeeded)
                {
                    return result.Merge(response);
                }

                var text = _text.Match(response.Value).Groups[1].Value;

                var numbers = _number.Matches(text);

                var datetime = numbers.ToList(x => x.Groups[1].Value ?? String.Empty).Join("/");

                var model = new ForecastModel()
                {
                    Url = url,
                    Type = type,
                    Label = datetime,
                    DateTime = DateTime.Now,
                };

                return result.Ok(model);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }
    }
}
