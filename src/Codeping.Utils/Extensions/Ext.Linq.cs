﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 列表操作
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 对集合随机排序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="source">集合</param>
        public static List<T> Sort<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return null;
            }

            Random random = new Random(Environment.TickCount);

            List<T> list = source.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                int index1 = random.Next(0, list.Count);
                int index2 = random.Next(0, list.Count);

                T temp = list[index1];
                list[index1] = list[index2];
                list[index2] = temp;
            }

            return list;
        }

        /// <summary>
        /// 将集合连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<T>(this IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            if (list == null)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (T each in list)
            {
                result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
            }

            if (separator == "")
            {
                return result.ToString();
            }

            return result.ToString().TrimEnd(separator.ToCharArray());
        }

        /// <summary>
        /// 转换可枚举接口为指定类型列表
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this IEnumerable source)
        {
            var result = new List<T>();

            foreach (T item in source)
            {
                result.Add(item);
            }

            return result;
        }
    }
}