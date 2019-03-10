using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - Http 上下文
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取Ip
        /// </summary>
        public static string GetIp(this HttpContext context)
        {
            string result = context?.GetWebClientIp();

            if (result.IsEmpty())
            {
                result = GetLanIp();
            }

            return result;
        }

        /// <summary>
        /// 获取 Web 客户端的 Ip
        /// </summary>
        private static string GetWebClientIp(this HttpContext context)
        {
            string ip = context.GetWebRemoteIp();

            foreach (IPAddress hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取局域网 IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (IPAddress hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取 Web 远程 Ip
        /// </summary>
        private static string GetWebRemoteIp(this HttpContext context)
        {
            string ip = context.GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();

            if (ip.IsEmpty() && context?.Connection?.RemoteIpAddress != null)
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }

            if (ip.IsEmpty())
            {
                ip = context.GetHeaderValueAs<string>("REMOTE_ADDR");
            }

            return ip;
        }

        private static T GetHeaderValueAs<T>(this HttpContext context, string headerName)
        {
            if (context?.Request?.Headers?.TryGetValue(headerName, out StringValues values) ?? false)
            {
                string rawValues = values.ToString();

                if (!rawValues.IsEmpty())
                {
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
                }
            }

            return default;
        }

        private static List<string> SplitCsv(this string csvList)
        {
            if (csvList.IsEmpty())
            {
                return new List<string>();
            }

            return csvList.TrimEnd(',').Split(',').AsEnumerable().Select(s => s.Trim()).ToList();
        }
    }
}
