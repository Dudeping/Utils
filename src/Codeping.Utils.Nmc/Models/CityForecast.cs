using System.Collections.Generic;

namespace Codeping.Utils.Nmc
{
    /// <summary>
    /// 城市天气
    /// </summary>
    public class CityForecast
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
}
