using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 加密操作 - RSA签名
    /// </summary>
    public static partial class Encrypt
    {
        /// <summary>
        /// RSA 加密，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        public static string RsaSign(string value, string key)
        {
            return RsaSign(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// RSA加密，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        public static string RsaSign(string value, string key, Encoding encoding)
        {
            return RsaSign(value, key, encoding, RSAType.RSA);
        }

        /// <summary>
        /// RSA 加密，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        public static string Rsa2Sign(string value, string key)
        {
            return Rsa2Sign(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// RSA加密，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        public static string Rsa2Sign(string value, string key, Encoding encoding)
        {
            return RsaSign(value, key, encoding, RSAType.RSA2);
        }

        /// <summary>
        /// Rsa 加密
        /// </summary>
        private static string RsaSign(string value, string key, Encoding encoding, RSAType type)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            RsaHelper rsa = new RsaHelper(type, encoding, key);
            return rsa.Sign(value);
        }

        /// <summary>
        /// Rsa验签，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        public static bool RsaVerify(string value, string publicKey, string sign)
        {
            return RsaVerify(value, publicKey, sign, Encoding.UTF8);
        }

        /// <summary>
        /// Rsa 验签，采用 SHA1 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">编码</param>
        public static bool RsaVerify(string value, string publicKey, string sign, Encoding encoding)
        {
            return RsaVerify(value, publicKey, sign, encoding, RSAType.RSA);
        }

        /// <summary>
        /// Rsa 验签，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        public static bool Rsa2Verify(string value, string publicKey, string sign)
        {
            return Rsa2Verify(value, publicKey, sign, Encoding.UTF8);
        }

        /// <summary>
        /// Rsa 验签，采用 SHA256 算法
        /// </summary>
        /// <param name="value">待验签的值</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="sign">签名</param>
        /// <param name="encoding">编码</param>
        public static bool Rsa2Verify(string value, string publicKey, string sign, Encoding encoding)
        {
            return RsaVerify(value, publicKey, sign, encoding, RSAType.RSA2);
        }

        /// <summary>
        /// Rsa验签
        /// </summary>
        private static bool RsaVerify(string value, string publicKey, string sign, Encoding encoding, RSAType type)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(publicKey) || string.IsNullOrWhiteSpace(sign))
            {
                return false;
            }

            RsaHelper rsa = new RsaHelper(type, encoding, publicKey: publicKey);
            return rsa.Verify(value, sign);
        }
    }
}
