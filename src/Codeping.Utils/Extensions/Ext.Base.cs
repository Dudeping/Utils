using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils
{
    public static partial class Ext
    {
        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default;
        }

        /// <summary>
        /// 安全转换为字符串, 去除两端空格, 当值为 null 时返回 ""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string SafeString(this object input)
        {
            return input?.ToString().Trim() ?? string.Empty;
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="input">输入值</param>
        public static T Cast<T>(this object input)
        {
            if (input == null)
            {
                return default;
            }

            if (input is string str && str.IsEmpty())
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
    }
}
