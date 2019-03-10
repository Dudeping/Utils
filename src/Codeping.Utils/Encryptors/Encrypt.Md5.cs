using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 加密操作 - Md5 加密
    /// </summary>
    public static partial class Encrypt
    {
        /// <summary>
        /// Md5 加密，返回 16 位结果
        /// </summary>
        /// <param name="value">值</param>
        public static string Md5By16(this string value)
        {
            return value.Md5By16(Encoding.UTF8);
        }

        /// <summary>
        /// Md5 加密，返回 16 位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By16(this string value, Encoding encoding)
        {
            return value.Md5(encoding, 4, 8);
        }

        /// <summary>
        /// Md5 加密
        /// </summary>
        private static string Md5(this string value, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                byte[] hash = md5.ComputeHash(encoding.GetBytes(value));
                result = startIndex == null ? BitConverter.ToString(hash) : BitConverter.ToString(hash, startIndex.SafeValue(), length.SafeValue());
            }
            finally
            {
                md5.Clear();
            }
            return result.Replace("-", "");
        }

        /// <summary>
        /// Md5 加密，返回 32 位结果
        /// </summary>
        /// <param name="value">值</param>
        public static string Md5By32(this string value)
        {
            return value.Md5By32(Encoding.UTF8);
        }

        /// <summary>
        /// Md5 加密，返回 32 位结果
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static string Md5By32(this string value, Encoding encoding)
        {
            return value.Md5(encoding, null, null);
        }
    }
}
