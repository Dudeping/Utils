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
    /// 反射操作
    /// </summary>
    public static class TypeEx
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return typeof(T).GetType();
        }

        /// <summary>
        /// 获取类型描述，使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDescription<T>()
        {
            return TypeEx.GetType<T>().GetDescription();
        }

        /// <summary>
        /// 获取类型成员描述，使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>(string memberName)
        {
            return TypeEx.GetType<T>().GetDescription(memberName);
        }

        /// <summary>
        /// 获取显示名称，使用 DisplayNameAttribute 设置显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayName<T>()
        {
            return TypeEx.GetType<T>().GetDisplayName();
        }

        /// <summary>
        /// 获取显示名称或描述,使用 DisplayNameAttribute 设置显示名称,使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayNameOrDescription<T>()
        {
            return TypeEx.GetType<T>().GetDisplayNameOrDescription();
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <typeparam name="TFind">查找类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<Type> FindTypes<TFind>(params Assembly[] assemblies)
        {
            Type findType = typeof(TFind);

            return FindTypes(findType, assemblies);
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <param name="findType">查找类型</param>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<Type> FindTypes(Type findType, params Assembly[] assemblies)
        {
            List<Type> result = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                result.AddRange(GetTypes(findType, assembly));
            }

            return result.Distinct().ToList();
        }

        /// <summary>
        /// 获取实现了接口的所有实例
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<TInterface> GetInstancesByInterface<TInterface>(params Assembly[] assemblies)
        {
            return FindTypes<TInterface>(assemblies)
                .Select(t => t.CreateInstance<TInterface>())
                .ToList();
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetTopBaseType<T>()
        {
            return typeof(T).GetTopBaseType();
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        private static List<Type> GetTypes(Type findType, Assembly assembly)
        {
            List<Type> result = new List<Type>();
            if (assembly == null)
            {
                return result;
            }

            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }
            foreach (Type type in types)
            {
                AddType(result, findType, type);
            }

            return result;
        }

        /// <summary>
        /// 添加类型
        /// </summary>
        private static void AddType(List<Type> result, Type findType, Type type)
        {
            if (type.IsInterface || type.IsAbstract)
            {
                return;
            }

            if (findType.IsAssignableFrom(type) == false && MatchGeneric(findType, type) == false)
            {
                return;
            }

            result.Add(type);
        }

        /// <summary>
        /// 泛型匹配
        /// </summary>
        private static bool MatchGeneric(Type findType, Type type)
        {
            if (findType.IsGenericTypeDefinition == false)
            {
                return false;
            }

            Type definition = findType.GetGenericTypeDefinition();
            foreach (Type implementedInterface in type.FindInterfaces((filter, criteria) => true, null))
            {
                if (implementedInterface.IsGenericType == false)
                {
                    continue;
                }

                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }
            return false;
        }
    }
}
