namespace Codeping.Utils.Logging
{
    public class LoggingOptions
    {
        /// <summary>
        /// 是否开启批量提交
        /// </summary>
        public bool EnableBatch { get; set; } = true;

        /// <summary>
        /// 批量提交的阈值
        /// </summary>
        public int Batch { get; set; } = 10;

        /// <summary>
        /// 设置强制提交队列中的数据的时间间隔(s), 当该间隔内未产生提交, 此设置在开启批量提交后生效
        /// </summary>
        public int Interval { get; set; } = 5;

        /// <summary>
        /// 是否合并到原因的日志中
        /// </summary>
        public bool IsMerge { get; set; } = true;
    }
}
