using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 字符串操作
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 按照指定的分隔符分割字符串, 并丢掉空节点
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">指定的分隔符</param>
        /// <returns></returns>
        public static string[] SplitWhitoutEmpty(this string str, params char[] separator)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new string[0];
            }

            if (separator == null || separator.Length == 0)
            {
                return new[] { str };
            }

            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 修正字符串分隔符
        /// </summary>
        /// <param name="str">要修正的字符串</param>
        /// <param name="separator">修正后的分隔符</param>
        /// <param name="oldSeparator">修正前的分隔符</param>
        /// <returns></returns>
        public static string CorrectSplitChar(this string str, char separator, params char[] oldSeparator)
        {
            var attrs = str.SplitWhitoutEmpty(oldSeparator);

            return string.Join(separator, attrs);
        }

        /// <summary>
        /// 修正分号分隔符
        /// </summary>
        /// <param name="str">要修正的字符串</param>
        /// <returns></returns>
        public static string CorrectQuotes(this string str)
        {
            return str.CorrectSplitChar(';', new[] { ';', '；' });
        }

        /// <summary>
        /// 获取汉字的拼音简码, 即首字母缩写, 范例：中国, 返回 zg
        /// </summary>
        /// <param name="chineseText">汉字文本, 范例： 中国</param>
        public static string PinYin(this string chineseText)
        {
            if (chineseText.IsEmpty())
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (char text in chineseText)
            {
                result.AppendFormat("{0}", text.ResolvePinYin());
            }

            return result.ToString().ToLower();
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(this string value)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }

            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(this string value)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }

            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="removeValue">要移除的值</param>
        public static string RemoveEnd(this string value, string removeValue)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }

            if (removeValue.IsEmpty())
            {
                return value.SafeString();
            }

            if (value.ToLower().EndsWith(removeValue.ToLower()))
            {
                return value.Remove(value.Length - removeValue.Length, removeValue.Length);
            }

            return value;
        }

        /// <summary>
        /// 解析单个汉字的拼音简码
        /// </summary>
        private static string ResolvePinYin(this char text)
        {
            var txt = text.SafeString();

            byte[] charBytes = Encoding.UTF8.GetBytes(txt);
            if (charBytes[0] <= 127)
            {
                return txt;
            }

            ushort unicode = (ushort)((charBytes[0] * 256) + charBytes[1]);
            string pinYin = unicode.ResolveByCode();
            if (!pinYin.IsEmpty())
            {
                return pinYin;
            }

            return txt.ResolveByConst();
        }

        /// <summary>
        /// 使用字符编码方式获取拼音简码
        /// </summary>
        private static string ResolveByCode(this ushort unicode)
        {
            if (unicode >= '\uB0A1' && unicode <= '\uB0C4')
            {
                return "A";
            }

            if (unicode >= '\uB0C5' && unicode <= '\uB2C0' && unicode != 45464)
            {
                return "B";
            }

            if (unicode >= '\uB2C1' && unicode <= '\uB4ED')
            {
                return "C";
            }

            if (unicode >= '\uB4EE' && unicode <= '\uB6E9')
            {
                return "D";
            }

            if (unicode >= '\uB6EA' && unicode <= '\uB7A1')
            {
                return "E";
            }

            if (unicode >= '\uB7A2' && unicode <= '\uB8C0')
            {
                return "F";
            }

            if (unicode >= '\uB8C1' && unicode <= '\uB9FD')
            {
                return "G";
            }

            if (unicode >= '\uB9FE' && unicode <= '\uBBF6')
            {
                return "H";
            }

            if (unicode >= '\uBBF7' && unicode <= '\uBFA5')
            {
                return "J";
            }

            if (unicode >= '\uBFA6' && unicode <= '\uC0AB')
            {
                return "K";
            }

            if (unicode >= '\uC0AC' && unicode <= '\uC2E7')
            {
                return "L";
            }

            if (unicode >= '\uC2E8' && unicode <= '\uC4C2')
            {
                return "M";
            }

            if (unicode >= '\uC4C3' && unicode <= '\uC5B5')
            {
                return "N";
            }

            if (unicode >= '\uC5B6' && unicode <= '\uC5BD')
            {
                return "O";
            }

            if (unicode >= '\uC5BE' && unicode <= '\uC6D9')
            {
                return "P";
            }

            if (unicode >= '\uC6DA' && unicode <= '\uC8BA')
            {
                return "Q";
            }

            if (unicode >= '\uC8BB' && unicode <= '\uC8F5')
            {
                return "R";
            }

            if (unicode >= '\uC8F6' && unicode <= '\uCBF9')
            {
                return "S";
            }

            if (unicode >= '\uCBFA' && unicode <= '\uCDD9')
            {
                return "T";
            }

            if (unicode >= '\uCDDA' && unicode <= '\uCEF3')
            {
                return "W";
            }

            if (unicode >= '\uCEF4' && unicode <= '\uD188')
            {
                return "X";
            }

            if (unicode >= '\uD1B9' && unicode <= '\uD4D0')
            {
                return "Y";
            }

            if (unicode >= '\uD4D1' && unicode <= '\uD7F9')
            {
                return "Z";
            }

            return string.Empty;
        }

        /// <summary>
        /// 通过拼音简码常量获取
        /// </summary>
        private static string ResolveByConst(this string text)
        {
            int index = Const.ChinesePinYin.IndexOf(text, StringComparison.Ordinal);
            if (index < 0)
            {
                return string.Empty;
            }

            return Const.ChinesePinYin.Substring(index + 1, 1);
        }
    }
}
