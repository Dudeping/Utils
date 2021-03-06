﻿using System;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统操作 - 枚举操作
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Name(this Enum instance)
        {
            return EnumEx.GetName(instance.Type(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int Value(this Enum instance)
        {
            return EnumEx.GetValue(instance.Type(), instance);
        }

        /// <summary>
        /// 获取枚举描述, 使用 System.ComponentModel.Description 特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description(this Enum instance)
        {
            var type = instance.Type();

            return type.GetDescription(EnumEx.GetName(type, instance));
        }
    }
}
