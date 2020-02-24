using System;

namespace Codeping.Nmc
{
    public class AlarmModel
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
}
