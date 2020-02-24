namespace Codeping.Nmc
{
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
        /// 移动端天气页
        /// </summary>
        public string MobileUrl { get; set; }

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
