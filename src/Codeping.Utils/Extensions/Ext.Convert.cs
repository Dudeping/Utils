using System;
using System.Collections.Generic;
using System.Linq;
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
            bool success = int.TryParse(input.SafeString(), out int r);
            if (success)
            {
                return r;
            }

            try
            {
                double? temp = ToDoubleOrNull(input, 0);

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
        /// 转换为 32 位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(this object input, int? digits = null)
        {
            return input.ToFloatOrNull(digits) ?? 0;
        }

        /// <summary>
        /// 转换为 32 位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(this object input, int? digits = null)
        {
            bool success = float.TryParse(input.SafeString(), out float r);

            if (!success)
            {
                return null;
            }

            if (digits == null)
            {
                return r;
            }

            return (float)Math.Round(r, digits.Value);
        }

        /// <summary>
        /// 转换为 64 位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object input, int? digits = null)
        {
            return input.ToDoubleOrNull(digits) ?? 0;
        }

        /// <summary>
        /// 转换为 64 位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(this object input, int? digits = null)
        {
            bool success = double.TryParse(input.SafeString(), out double r);

            if (!success)
            {
                return null;
            }

            if (digits == null)
            {
                return r;
            }

            return Math.Round(r, digits.Value);
        }

        /// <summary>
        /// 转换为 128 位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object input, int? digits = null)
        {
            return ToDecimalOrNull(input, digits) ?? 0;
        }

        /// <summary>
        /// 转换为 128 位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object input, int? digits = null)
        {
            bool success = decimal.TryParse(input.SafeString(), out decimal r);

            if (!success)
            {
                return null;
            }

            if (digits == null)
            {
                return r;
            }

            return Math.Round(r, digits.Value);
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
            bool success = long.TryParse(input.SafeString(), out long r);

            if (success)
            {
                return r;
            }

            try
            {
                decimal? temp = input.ToDecimalOrNull(0);
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
            bool? value = input.GetBool();

            if (value != null)
            {
                return value.Value;
            }

            return bool.TryParse(input.SafeString(), out bool r) ? (bool?)r : null;
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
            return DateTime.TryParse(input.SafeString(), out DateTime r) ? (DateTime?)r : null;
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
            return Guid.TryParse(input.SafeString(), out Guid r) ? (Guid?)r : null;
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>        
        public static byte[] ToBytes(this string input)
        {
            return ToBytes(input, Encoding.UTF8);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(this string input, Encoding encoding)
        {
            return string.IsNullOrWhiteSpace(input) ? new byte[] { } : encoding.GetBytes(input);
        }

        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default;
        }

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string SafeString(this object input)
        {
            return input?.ToString().Trim() ?? string.Empty;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
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
            return value == Guid.Empty ? true : false;
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="input">以逗号分隔的Guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<Guid> ToGuidList(string input)
        {
            return input.ToList<Guid>();
        }

        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="input">以逗号分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>(this string input)
        {
            List<T> r = new List<T>();

            if (string.IsNullOrWhiteSpace(input))
            {
                return r;
            }

            string[] array = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return array.Select(x => x.To<T>()).ToList();
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="input">输入值</param>
        public static T To<T>(this object input)
        {
            if (input == null)
            {
                return default;
            }

            if (input is string && string.IsNullOrWhiteSpace(input.ToString()))
            {
                return default;
            }

            Type type = TypeEx.GetType<T>();
            string typeName = type.Name.ToLower();

            try
            {
                if (typeName == "string")
                {
                    return (T)(object)input.ToString();
                }

                if (typeName == "guid")
                {
                    return (T)(object)new Guid(input.ToString());
                }

                if (type.IsEnum)
                {
                    return (T)Enum.Parse(TypeEx.GetType<T>(), input.SafeString(), true);
                }

                if (input is IConvertible)
                {
                    return (T)Convert.ChangeType(input, type);
                }

                return (T)input;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
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
