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
            return typeof(T).PureType();
        }

        /// <summary>
        /// 获取类型描述, 使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDescription<T>()
        {
            return TypeEx.GetType<T>().GetDescription();
        }

        /// <summary>
        /// 获取类型成员描述, 使用 DescriptionAttribute 设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>(string memberName)
        {
            return TypeEx.GetType<T>().GetDescription(memberName);
        }

        /// <summary>
        /// 获取显示名称, 使用 DisplayNameAttribute 设置显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayName<T>()
        {
            return TypeEx.GetType<T>().GetDisplayName();
        }

        /// <summary>
        /// 获取显示名称或描述, 使用 DisplayNameAttribute 设置显示名称, 使用 DescriptionAttribute 设置描述
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
        public static IEnumerable<Type> FindTypes<TFind>(params Assembly[] assemblies)
        {
            return TypeEx.FindTypes(TypeEx.GetType<TFind>(), assemblies);
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <param name="findType">查找类型</param>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static IEnumerable<Type> FindTypes(Type findType, params Assembly[] assemblies)
        {
            var result = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                result.AddRange(TypeEx.GetTypes(findType, assembly));
            }

            return result.Distinct();
        }

        /// <summary>
        /// 获取实现了接口的所有实例
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static IEnumerable<TInterface> GetInstancesByInterface<TInterface>(params Assembly[] assemblies)
        {
            return TypeEx.FindTypes<TInterface>(assemblies).Select(x => x.CreateInstance<TInterface>());
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetTopBaseType<T>()
        {
            return TypeEx.GetType<T>().GetTopBaseType();
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        /// <summary>
        /// 获取程序集的直接依赖程序集, 包括该程序集本身
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetDependencyAssemblies(string assemblyName)
        {
            var result = new List<Assembly>();

            var assembly = TypeEx.GetAssembly(assemblyName);

            result.Add(assembly);

            var context = DependencyContext.Load(assembly);

            if (context == null)
            {
                var dependencies = context.RuntimeLibraries
                     .SelectMany(x => x.Dependencies)
                     .Select(x => TypeEx.GetAssembly(x.Name));

                result.AddRange(dependencies);
            }

            return result;
        }

        /// <summary>
        /// 获取类型列表
        /// </summary>
        private static IEnumerable<Type> GetTypes(Type findType, Assembly assembly)
        {
            var result = new List<Type>();

            if (assembly == null)
            {
                return result;
            }

            try
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }

                    if (!findType.IsAssignableFrom(type) &&
                        !TypeEx.MatchGeneric(findType, type))
                    {
                        continue;
                    }

                    result.Add(type);
                }

                return result;
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }
        }

        /// <summary>
        /// 泛型匹配
        /// </summary>
        private static bool MatchGeneric(Type findType, Type type)
        {
            if (!findType.IsGenericTypeDefinition)
            {
                return false;
            }

            Type definition = findType.GetGenericTypeDefinition();

            foreach (Type implementedInterface in type.FindInterfaces((f, c) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                {
                    continue;
                }

                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }

            return false;
        }
    }
}
