﻿using System;

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
            return input?.ToString().Trim() ?? String.Empty;
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

            var type = TypeEx.GetType<T>();
            var typeName = type.Name.ToLower();

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
        /// 改变对象的类型
        /// </summary>
        /// <param name="input">要操作的对象</param>
        /// <param name="type">要更改到的类型</param>
        /// <returns></returns>
        public static object ChangeType(this object input, Type type)
        {
            if (input is string str && str.IsEmpty())
            {
                return default;
            }

            var typeName = type.Name.ToLower();

            try
            {
                if (typeName == "string")
                {
                    return (object)input.ToString();
                }

                if (typeName == "guid")
                {
                    return (object)new Guid(input.ToString());
                }

                if (type.IsEnum)
                {
                    return Enum.Parse(type, input.SafeString(), true);
                }

                if (input is IConvertible)
                {
                    return Convert.ChangeType(input, type);
                }

                return input;
            }
            catch
            {
                return default;
            }
        }
    }
}
