using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 反射
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取方法的全名称
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns></returns>
        public static string GetFullName([NotNull]this MethodBase method)
        {
            return $"{method.DeclaringType.FullName}.{method.Name}";
        }

        /// <summary>
        /// 获取实例上的属性值
        /// </summary>
        /// <param name="member">成员信息</param>
        /// <param name="instance">成员所在的类实例</param>
        public static object GetPropertyValue([NotNull]this MemberInfo member, [NotNull]object instance)
        {
            return instance.Type().GetProperty(member.Name)?.GetValue(instance);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static Type PureType([NotNull]this Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static Type Type([NotNull]this object obj)
        {
            return obj.GetType().PureType();
        }

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>(this Type type, params object[] parameters)
        {
            return Activator.CreateInstance(type, parameters).Cast<T>();
        }

        /// <summary>
        /// 是否是元组类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsTupleType(this Type type)
        {
            switch (type.GetGenericArguments().Length)
            {
                case 1: return type.GetGenericTypeDefinition() == typeof(Tuple<>);
                case 2: return type.GetGenericTypeDefinition() == typeof(Tuple<,>);
                case 3: return type.GetGenericTypeDefinition() == typeof(Tuple<,,>);
                case 4: return type.GetGenericTypeDefinition() == typeof(Tuple<,,,>);
                case 5: return type.GetGenericTypeDefinition() == typeof(Tuple<,,,,>);
                case 6: return type.GetGenericTypeDefinition() == typeof(Tuple<,,,,,>);
                case 7: return type.GetGenericTypeDefinition() == typeof(Tuple<,,,,,,>);
            }

            return false;
        }

        /// <summary>
        /// 是否直接依赖于指定程序集
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="dependencyAssemblyName">依赖程序集名称</param>
        /// <returns></returns>
        public static bool IsDependency(this Assembly assembly, string dependencyAssemblyName)
        {
            var context = DependencyContext.Load(assembly);

            if (context == null)
            {
                return false;
            }

            return context.RuntimeLibraries.Any(
                x => x.Dependencies.Any(d => d.Name == dependencyAssemblyName));
        }

        /// <summary>
        /// 是否直接依赖于指定程序集
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="dependencyAssembly">依赖程序集</param>
        /// <returns></returns>
        public static bool IsDependency(this Assembly assembly, Assembly dependencyAssembly)
        {
            var context = DependencyContext.Load(assembly);

            if (context == null)
            {
                return false;
            }

            return context.RuntimeLibraries.Any(
                x => x.Dependencies.Any(d => d.Name == dependencyAssembly.GetName().Name));
        }

        /// <summary>
        /// 判断类型是否可空
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool(this MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Boolean";
                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }

            return false;
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsBool(this PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum(this MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;
                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        private static bool IsEnum(this PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
            {
                return true;
            }

            Type value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
            {
                return false;
            }

            return value.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate(this MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.DateTime";
                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        private static bool IsDate(this PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime))
            {
                return true;
            }

            if (property.PropertyType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt(this MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Int16"
                        || member.ToString() == "System.Int32"
                        || member.ToString() == "System.Int64";
                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        private static bool IsInt(this PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
            {
                return true;
            }

            if (property.PropertyType == typeof(int?))
            {
                return true;
            }

            if (property.PropertyType == typeof(short))
            {
                return true;
            }

            if (property.PropertyType == typeof(short?))
            {
                return true;
            }

            if (property.PropertyType == typeof(long))
            {
                return true;
            }

            if (property.PropertyType == typeof(long?))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber(this MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            if (IsInt(member))
            {
                return true;
            }

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Single"
                        || member.ToString() == "System.Decimal"
                        || member.ToString() == "System.Double";
                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否数值类型
        /// </summary>
        private static bool IsNumber(this PropertyInfo property)
        {
            if (property.PropertyType == typeof(double))
            {
                return true;
            }

            if (property.PropertyType == typeof(double?))
            {
                return true;
            }

            if (property.PropertyType == typeof(decimal))
            {
                return true;
            }

            if (property.PropertyType == typeof(decimal?))
            {
                return true;
            }

            if (property.PropertyType == typeof(float))
            {
                return true;
            }

            if (property.PropertyType == typeof(float?))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsCollection(this Type type)
        {
            return type.IsArray || type.IsGenericCollection();
        }

        /// <summary>
        /// 是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsGenericCollection(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            Type typeDefinition = type.GetGenericTypeDefinition();

            return typeDefinition == typeof(IEnumerable<>)
                || typeDefinition == typeof(IReadOnlyCollection<>)
                || typeDefinition == typeof(IReadOnlyList<>)
                || typeDefinition == typeof(ICollection<>)
                || typeDefinition == typeof(IList<>)
                || typeDefinition == typeof(List<>);
        }

        /// <summary>
        /// 从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static IEnumerable<Assembly> GetAssemblies(this string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                .Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"))
                .ToList(path => Assembly.Load(new AssemblyName(path)));
        }

        /// <summary>
        /// 获取公共属性列表
        /// </summary>
        /// <param name="instance">实例</param>
        public static IEnumerable<Item> GetPublicProperties(this object instance)
        {
            return instance.Type().GetProperties().ToList(t => new Item(t.Name, t.GetValue(instance)));
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetTopBaseType(this Type type)
        {
            if (type == null)
            {
                return null;
            }

            if (type.IsInterface)
            {
                return type;
            }

            if (type.BaseType == typeof(object))
            {
                return type;
            }

            return type.BaseType.GetTopBaseType();
        }

        /// <summary>
        /// 获取类型成员描述, 使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription(this Type type, string memberName)
        {
            if (type == null ||
                memberName.IsEmpty())
            {
                return string.Empty;
            }

            return type.GetTypeInfo().GetMember(memberName).FirstOrDefault().GetDescription();
        }

        /// <summary>
        /// 获取类型成员描述, 使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <param name="member">成员</param>
        public static string GetDescription(this MemberInfo member)
        {
            if (member == null)
            {
                return string.Empty;
            }

            return member.GetCustomAttribute<DescriptionAttribute>()?.Description ?? member.Name;
        }

        /// <summary>
        /// 获取显示名称, 使用 DisplayAttribute 或 DisplayNameAttribute 设置显示名称
        /// </summary>
        public static string GetDisplayName(this MemberInfo member)
        {
            if (member == null)
            {
                return string.Empty;
            }

            if (member.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute displayAttribute)
            {
                return displayAttribute.Name;
            }

            if (member.GetCustomAttribute<DisplayNameAttribute>() is DisplayNameAttribute displayNameAttribute)
            {
                return displayNameAttribute.DisplayName;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取属性显示名称或描述, 使用 DisplayAttribute 或 DisplayNameAttribute 设置显示名称, 使用 DescriptionAttribute 设置描述
        /// </summary>
        public static string GetDisplayNameOrDescription(this MemberInfo member)
        {
            string result = member.GetDisplayName();

            return result.IsEmpty() ? member.GetDescription() : result;
        }
    }
}
