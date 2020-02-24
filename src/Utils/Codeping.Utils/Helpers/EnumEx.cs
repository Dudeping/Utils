using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeping.Utils
{
    public static class EnumEx
    {
        /// <summary>
        /// 获取项集合, 文本设置为 Description, 值为 Value
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public static IEnumerable<Item> GetItems<TEnum>() where TEnum : Enum
        {
            var type = TypeEx.GetType<TEnum>();

            var result = new List<Item>();

            foreach (var field in type.GetFields())
            {
                if (!field.FieldType.IsEnum)
                {
                    continue;
                }

                var value = EnumEx.GetValue<TEnum>(field.Name);

                var description = field.GetDescription();

                result.Add(new Item(description, value, value));
            }

            return result.OrderBy(t => t.SortId).ToList();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名或值, 范例: Enum1 枚举有成员 A = 0, 则传入 "A" 或 "0" 获取 Enum1.A</param>
        public static TEnum Parse<TEnum>(object member) where TEnum : Enum
        {
            var value = member.SafeString();

            var type = TypeEx.GetType<TEnum>();

            if (value.IsEmpty())
            {
                if (type.IsGenericType)
                {
                    return default;
                }

                throw new ArgumentNullException(nameof(member));
            }

            return (TEnum)Enum.Parse(type, value, true);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可, 范例: Enum1 枚举有成员 A = 0, 则传入 Enum1.A 或 0, 获取成员名 "A" </param>
        public static string GetName<TEnum>(object member) where TEnum : Enum
        {
            return EnumEx.GetName(TypeEx.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static int GetValue<TEnum>(object member) where TEnum : Enum
        {
            return EnumEx.GetValue(TypeEx.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取描述, 使用 System.ComponentModel.Description 特性设置描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetDescription<TEnum>(object member) where TEnum : Enum
        {
            return TypeEx.GetDescription<TEnum>(EnumEx.GetName<TEnum>(member));
        }

        internal static int GetValue(Type type, object member)
        {
            var value = member.SafeString();

            if (value.IsEmpty())
            {
                throw new ArgumentNullException(nameof(member));
            }

            return (int)Enum.Parse(type, member.ToString(), true);
        }

        internal static string GetName(Type type, object member)
        {
            if (type == null || member == null)
            {
                return String.Empty;
            }

            if (member is string)
            {
                return member.ToString();
            }

            return Enum.GetName(type, member);
        }
    }
}
