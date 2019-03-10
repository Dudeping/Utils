namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 格式化
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value">布尔值</param>
        public static string Description(this bool value)
        {
            return value ? "是" : "否";
        }
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value">布尔值</param>
        public static string Description(this bool? value)
        {
            return value == null ? "" : Description(value.Value);
        }
        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="defaultValue">空值显示的默认文本</param>
        public static string Format(this int number, string defaultValue = "")
        {
            if (number == 0)
                return defaultValue;
            return number.ToString();
        }
        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="defaultValue">空值显示的默认文本</param>
        public static string Format(this int? number, string defaultValue = "")
        {
            return number.SafeValue().Format(defaultValue);
        }
        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="defaultValue">空值显示的默认文本</param>
        public static string Format(this decimal number, string defaultValue = "")
        {
            if (number == 0)
                return defaultValue;
            return string.Format("{0:0.##}", number);
        }
        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="defaultValue">空值显示的默认文本</param>
        public static string Format(this decimal? number, string defaultValue = "")
        {
            return number.SafeValue().Format(defaultValue);
        }
        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="defaultValue">空值显示的默认文本</param>
        public static string Format(this double number, string defaultValue = "")
        {
            if (number == 0)
                return defaultValue;
            return string.Format("{0:0.##}", number);
        }
        /// <summary>
        /// 获取格式化字符串
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="defaultValue">空值显示的默认文本</param>
        public static string Format(this double? number, string defaultValue = "")
        {
            return Format(number.SafeValue(), defaultValue);
        }
        /// <summary>
        /// 获取格式化字符串,带￥
        /// </summary>
        /// <param name="number">数值</param>
        public static string Rmb(this decimal number)
        {
            if (number == 0)
                return "￥0";
            return string.Format("￥{0:0.##}", number);
        }
        /// <summary>
        /// 获取格式化字符串,带￥
        /// </summary>
        /// <param name="number">数值</param>
        public static string Rmb(this decimal? number)
        {
            return number.SafeValue().Rmb();
        }
        /// <summary>
        /// 获取格式化字符串,带%
        /// </summary>
        /// <param name="number">数值</param>
        public static string Percent(this decimal number)
        {
            if (number == 0)
                return string.Empty;
            return string.Format("{0:0.##}%", number);
        }
        /// <summary>
        /// 获取格式化字符串,带%
        /// </summary>
        /// <param name="number">数值</param>
        public static string Percent(this decimal? number)
        {
            return number.SafeValue().Percent();
        }
        /// <summary>
        /// 获取格式化字符串,带%
        /// </summary>
        /// <param name="number">数值</param>
        public static string Percent(this double number)
        {
            if (number == 0)
                return string.Empty;
            return string.Format("{0:0.##}%", number);
        }
        /// <summary>
        /// 获取格式化字符串,带%
        /// </summary>
        /// <param name="number">数值</param>
        public static string Percent(this double? number)
        {
            return number.SafeValue().Percent();
        }
    }
}
