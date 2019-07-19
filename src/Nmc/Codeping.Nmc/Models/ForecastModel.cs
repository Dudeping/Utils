using System;

namespace Codeping.Utils.Nmc
{
    /// <summary>
    /// 返回类型
    /// </summary>
    public class ForecastModel
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
        /// <summary>
        /// 预报类型
        /// </summary>
        public ForecastType Type { get; set; }
    }
}
