using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 类型转换
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 转换为 32 位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static int ToInt(this object input)
        {
            return input.ToIntOrNull() ?? 0;
        }

        /// <summary>
        /// 转换为 32 位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static int? ToIntOrNull(this object input)
        {
            if (Int32.TryParse(input.SafeString(), out var r))
            {
                return r;
            }

            try
            {
                var temp = input.ToDoubleOrNull(0);

                if (temp == null)
                {
                    return null;
                }

                return Convert.ToInt32(temp);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为 64 位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long ToLong(this object input)
        {
            return input.ToLongOrNull() ?? 0;
        }

        /// <summary>
        /// 转换为 64 位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long? ToLongOrNull(this object input)
        {
            if (Int64.TryParse(input.SafeString(), out var r))
            {
                return r;
            }

            try
            {
                var temp = input.ToDecimalOrNull(0);

                if (temp == null)
                {
                    return null;
                }

                return Convert.ToInt64(temp);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为 32 位浮点型, 并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(this object input, int? digits = null)
        {
            return input.ToFloatOrNull(digits) ?? 0;
        }

        /// <summary>
        /// 转换为 32 位可空浮点型, 并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(this object input, int? digits = null)
        {
            if (Single.TryParse(input.SafeString(), out var r))
            {
                if (digits != null)
                {
                    return (float)Math.Round(r, digits.Value);
                }
                else
                {
                    return r;
                }
            }

            return null;
        }

        /// <summary>
        /// 转换为 64 位浮点型, 并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object input, int? digits = null)
        {
            return input.ToDoubleOrNull(digits) ?? 0;
        }

        /// <summary>
        /// 转换为 64 位可空浮点型, 并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(this object input, int? digits = null)
        {
            if (Double.TryParse(input.SafeString(), out var r))
            {
                if (digits != null)
                {
                    return Math.Round(r, digits.Value);
                }
                else
                {
                    return r;
                }
            }

            return null;
        }

        /// <summary>
        /// 转换为 128 位浮点型, 并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object input, int? digits = null)
        {
            return input.ToDecimalOrNull(digits) ?? 0;
        }

        /// <summary>
        /// 转换为 128 位可空浮点型, 并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object input, int? digits = null)
        {
            if (Decimal.TryParse(input.SafeString(), out var r))
            {
                if (digits != null)
                {
                    return Math.Round(r, digits.Value);
                }
                else
                {
                    return r;
                }
            }

            return null;
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool ToBool(this object input)
        {
            return input.ToBoolOrNull() ?? false;
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool? ToBoolOrNull(this object input)
        {
            var value = input.GetBool();

            if (value != null)
            {
                return value.Value;
            }

            return Boolean.TryParse(input.SafeString(), out var r) ? (bool?)r : null;
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime ToDate(this object input)
        {
            return input.ToDateOrNull() ?? DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime? ToDateOrNull(this object input)
        {
            return DateTime.TryParse(input.SafeString(), out var r) ? (DateTime?)r : null;
        }

        /// <summary>
        /// 转换为 Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static Guid ToGuid(this object input)
        {
            return input.ToGuidOrNull() ?? Guid.Empty;
        }

        /// <summary>
        /// 转换为可空 Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static Guid? ToGuidOrNull(this object input)
        {
            return Guid.TryParse(input.SafeString(), out var r) ? (Guid?)r : null;
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>        
        public static byte[] ToBytes(this string input)
        {
            return input.ToBytes(Encoding.UTF8);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(this string input, Encoding encoding)
        {
            return input.IsEmpty() ? new byte[] { } : encoding.GetBytes(input);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid? value)
        {
            return value == null ? true : value.Value.IsEmpty();
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
            return value == Guid.Empty;
        }

        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="input">以指定分割符分割的元素的原数据</param>
        public static List<T> ToList<T>(this string input, string separator)
        {
            if (input.IsEmpty())
            {
                return new List<T>();
            }

            return input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                .ToList(x => x.Cast<T>());
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="input">原数据</param>
        /// <returns></returns>
        private static bool? GetBool(this object input)
        {
            switch (input.SafeString().ToLower())
            {
                case "0":
                    return false;
                case "否":
                    return false;
                case "不":
                    return false;
                case "no":
                    return false;
                case "fail":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "ok":
                    return true;
                case "yes":
                    return true;
                default:
                    return null;
            }
        }
    }
}
