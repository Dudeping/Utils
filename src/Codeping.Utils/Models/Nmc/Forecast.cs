using System.Collections.Generic;

namespace Codeping.Utils
{
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
}
