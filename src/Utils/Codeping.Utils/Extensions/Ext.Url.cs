using Codeping.Utils;
using System.IO;
using System.Linq;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - Url 操作
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 合并 Url
        /// </summary>
        /// <param name="urls">url片断, 范例：Url.Combine( "http://a.com", "b" ), 返回 "http://a.com/b"</param>
        public static string Combine([NotNull]this string root, [NotNull]params string[] urls)
        {
            var list = urls.ToList();

            list.Insert(0, root);

            return Path.Combine(list.ToArray());
        }

        /// <summary>
        /// 连接 Url, 范例：Url.Join( "http://a.com", "b=1" ), 返回 "http://a.com?b=1"
        /// </summary>
        /// <param name="url">Url, 范例：http://a.com</param>
        /// <param name="param">参数, 范例：b=1</param>
        public static string CombineParam([NotNull]this string url, [NotNull]string param)
        {
            return $"{Ext.GetUrl(url)}{param}";
        }

        /// <summary>
        /// 获取 Url
        /// </summary>
        private static string GetUrl(string url)
        {
            if (!url.Contains("?"))
            {
                return $"{url}?";
            }

            if (url.EndsWith("?") ||
                url.EndsWith("&"))
            {
                return url;
            }

            return $"{url}&";
        }
    }
}
