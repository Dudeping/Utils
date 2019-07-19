using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 常用类库 - IP 转地址
    /// </summary>
    public static partial class Util
    {
        public static string GetLocation(string ip)
        {
            string res;

            try
            {
                string url = "http://apis.juhe.cn/ip/ip2addr?ip=" + ip + "&dtype=json&key=b39857e36bee7a305d55cdb113a9d725";
                res = Util.HttpGet(url);
                objex resjson = res.ToObject<objex>();
                res = resjson.result.area + " " + resjson.result.location;
            }
            catch (Exception)
            {
                res = "";
            }

            if (!res.IsEmpty()) return res;

            try
            {
                string url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=" + ip + "&resource_id=6006&ie=utf8&oe=gbk&format=json";
                res = Util.HttpGet(url, Encoding.GetEncoding("GBK"));
                obj resjson = res.ToObject<obj>();
                res = resjson.data[0].location;
            }
            catch (Exception)
            {
                res = "";
            }

            if (!res.IsEmpty()) return res;

            return "本地局域网";
        }

        private static string HttpGet(string url, Encoding encodeing = null)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request.ProtocolVersion = HttpVersion.Version10;

                // 忽略 ssl
                request.ServerCertificateValidationCallback =
                    (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true;
            }

            request.Method = "GET";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = request.GetResponse();

            if (response != null)
            {
                using (Stream rs = response.GetResponseStream())
                using (StreamReader sr = new StreamReader(rs, encodeing ?? Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }

            return null;
        }


        /// <summary>
        /// 百度接口
        /// </summary>
        class obj
        {
            public List<dataone> data { get; set; }
        }
        public class dataone
        {
            public string location { get; set; }
        }

        /// <summary>
        /// 聚合数据
        /// </summary>
        class objex
        {
            public string resultcode { get; set; }
            public dataoneex result { get; set; }
            public string reason { get; set; }
            public string error_code { get; set; }
        }

        class dataoneex
        {
            public string area { get; set; }
            public string location { get; set; }
        }
    }
}
