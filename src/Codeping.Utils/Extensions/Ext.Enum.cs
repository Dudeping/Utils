using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统操作 - 枚举操作
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名或值, 范例: Enum1 枚举有成员 A = 0, 则传入 "A" 或 "0" 获取 Enum1.A</param>
        public static TEnum Parse<TEnum>(this object member) where TEnum : Enum
        {
            string value = member.SafeString();

            if (string.IsNullOrWhiteSpace(value))
            {
                if (typeof(TEnum).IsGenericType)
                {
                    return default;
                }

                throw new ArgumentNullException(nameof(member));
            }

            return (TEnum)Enum.Parse(TypeEx.GetType<TEnum>(), value, true);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可,范例: Enum1 枚举有成员 A = 0, 则传入 Enum1.A 或 0, 获取成员名 "A" </param>
        public static string GetName<TEnum>(this object member) where TEnum : Enum
        {
            return Ext.GetName(TypeEx.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static int GetValue<TEnum>(this object member) where TEnum : Enum
        {
            return Ext.GetValue(TypeEx.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int Value(this Enum instance)
        {
            return Ext.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description(this Enum instance)
        {
            var type = instance.GetType();

            return type.GetDescription(Ext.GetName(type, instance));
        }

        /// <summary>
        /// 获取描述,使用 System.ComponentModel.Description 特性设置描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetDescription<TEnum>(this object member) where TEnum : Enum
        {
            return TypeEx.GetDescription<TEnum>(member.GetName<TEnum>());
        }

        /// <summary>
        /// 获取项集合,文本设置为 Description，值为 Value
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public static List<Item> GetItems<TEnum>() where TEnum : Enum
        {
            Type type = TypeEx.GetType<TEnum>();

            List<Item> result = new List<Item>();

            foreach (FieldInfo field in type.GetFields())
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }

                int value = field.Name.GetValue<TEnum>();

                string description = field.GetDescription();

                result.Add(new Item(description, value, value));
            }

            return result.OrderBy(t => t.SortId).ToList();
        }

        private static int GetValue(Type type, object member)
        {
            string value = member.SafeString();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(member));
            }

            return (int)Enum.Parse(type, member.ToString(), true);
        }

        private static string GetName(Type type, object member)
        {
            if (type == null ||
                member == null)
            {
                return string.Empty;
            }

            if (member is string)
            {
                return member.ToString();
            }

            return Enum.GetName(type, member);
        }
    }
}
