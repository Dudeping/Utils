using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Codeping.Utils.Mvc
{
    /// <summary>
    /// 系统扩展 - Http 上下文
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 获取 Ip
        /// </summary>
        public static string GetIp(this HttpContext context)
        {
            var result = context?.GetWebClientIp();

            return result.IsEmpty() ? GetLanIp() : result;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="name">文件提交表单项名</param>
        /// <param name="rootDir">保存的根路径</param>
        /// <returns></returns>
        public static string SaveFileAs(this HttpRequest request, string name, string rootDir)
        {
            var file = request.Form.Files.GetFile(name);

            if (file == null)
            {
                return "";
            }

            var fileExt = Path.GetExtension(file.FileName);

            var fileName = RandomEx.GenerateGuid() + fileExt;

            var relativePath = "/upload/" + fileName;

            var filePath = rootDir + relativePath;

            var directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                file.CopyTo(stream);
            }

            return relativePath;
        }

        /// <summary>
        /// 获取 Web 客户端的 Ip
        /// </summary>
        private static string GetWebClientIp(this HttpContext context)
        {
            var ip = context.GetWebRemoteIp();

            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// 获取局域网 IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// 获取 Web 远程 Ip
        /// </summary>
        private static string GetWebRemoteIp(this HttpContext context)
        {
            var ip = context.GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();

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
            if (context?.Request?.Headers?.TryGetValue(headerName, out var values) ?? false)
            {
                var rawValues = values.ToString();

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

            return csvList.TrimEnd(',').Split(',').ToList(s => s.Trim());
        }
    }
}
