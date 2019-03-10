using System;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 时间
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime dateTime, bool isRemoveSecond = false)
        {
            return isRemoveSecond ? dateTime.ToString("yyyy-MM-dd HH:mm") : dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false)
        {
            return dateTime == null ? string.Empty : dateTime.SafeValue().ToDateTimeString(isRemoveSecond);
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToDateString(this DateTime? dateTime)
        {
            return dateTime == null ? string.Empty : dateTime.SafeValue().ToDateString();
        }

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 获取格式化字符串，不带年月日，格式："HH:mm:ss"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToTimeString(this DateTime? dateTime)
        {
            return dateTime == null ? string.Empty : dateTime.SafeValue().ToTimeString();
        }

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 获取格式化字符串，带毫秒，格式："yyyy-MM-dd HH:mm:ss.fff"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToMillisecondString(this DateTime? dateTime)
        {
            return dateTime == null ? string.Empty : dateTime.SafeValue().ToMillisecondString();
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime dateTime)
        {
            return string.Format("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// 获取格式化字符串，不带时分秒，格式："yyyy年MM月dd日"
        /// </summary>
        /// <param name="dateTime">日期</param>
        public static string ToChineseDateString(this DateTime? dateTime)
        {
            return dateTime == null ? string.Empty : dateTime.SafeValue().ToChineseDateString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime dateTime, bool isRemoveSecond = false)
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("{0}年{1}月{2}日", dateTime.Year, dateTime.Month, dateTime.Day);
            result.AppendFormat(" {0}时{1}分", dateTime.Hour, dateTime.Minute);

            if (isRemoveSecond == false) result.AppendFormat("{0}秒", dateTime.Second);

            return result.ToString();
        }

        /// <summary>
        /// 获取格式化字符串，带时分秒，格式："yyyy年MM月dd日 HH时mm分"
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <param name="isRemoveSecond">是否移除秒</param>
        public static string ToChineseDateTimeString(this DateTime? dateTime, bool isRemoveSecond = false)
        {
            return dateTime == null ? string.Empty : dateTime.SafeValue().ToChineseDateTimeString(isRemoveSecond);
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="span">时间间隔</param>
        public static string Description(this TimeSpan span)
        {
            StringBuilder result = new StringBuilder();
            if (span.Days > 0)
            {
                result.AppendFormat("{0}天", span.Days);
            }

            if (span.Hours > 0)
            {
                result.AppendFormat("{0}小时", span.Hours);
            }

            if (span.Minutes > 0)
            {
                result.AppendFormat("{0}分", span.Minutes);
            }

            if (span.Seconds > 0)
            {
                result.AppendFormat("{0}秒", span.Seconds);
            }

            if (span.Milliseconds > 0)
            {
                result.AppendFormat("{0}毫秒", span.Milliseconds);
            }

            if (result.Length > 0)
            {
                return result.ToString();
            }

            return $"{span.TotalSeconds * 1000}毫秒";
        }

        /// <summary>
        /// 获取 Unix 时间戳
        /// </summary>
        /// <param name="time">时间</param>
        public static long GetUnixTimestamp(this DateTime time)
        {
            DateTime start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);

            long ticks = (time - start.Add(new TimeSpan(8, 0, 0))).Ticks;

            return (ticks / TimeSpan.TicksPerSecond).ToLong();
        }

        /// <summary>
        /// 从 Unix 时间戳获取时间
        /// </summary>
        /// <param name="timestamp"> Unix 时间戳</param>
        public static DateTime GetTimeFromUnixTimestamp(this long timestamp)
        {
            DateTime start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);

            TimeSpan span = new TimeSpan(long.Parse(timestamp + "0000000"));

            return start.Add(span).Add(new TimeSpan(8, 0, 0));
        }
    }
}
