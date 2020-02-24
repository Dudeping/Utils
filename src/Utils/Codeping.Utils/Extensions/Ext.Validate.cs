using System.Text;
using System.Text.RegularExpressions;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - 校验
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(this string stringValue)
        {
            return Encoding.Default.GetBytes(stringValue).Length;
        }

        /// <summary>
        /// 检测用户名格式是否有效
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsValidUserName(this string userName)
        {
            // 判断用户名的长度（4-20个字符）及内容（只能是汉字、字母、下划线、数字）是否合法
            var userNameLength = GetStringLength(userName);

            return userNameLength >= 4 && userNameLength <= 20 && Regex.IsMatch(userName, @"^([\u4e00-\u9fa5A-Za-z_0-9]{0, })$");
        }

        /// <summary>
        /// 密码有效性
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPassword(this string password)
        {
            return Regex.IsMatch(password, @"^[A-Za-z_0-9]{6, 16}$");
        }

        /// <summary>
        /// int有效性
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public bool IsValidInt(this string val)
        {
            return Regex.IsMatch(val, @"^[1-9]\d*\.?[0]*$");
        }

        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(this string inputData)
        {
            return Regex.IsMatch(inputData, @"^(-?\d*)(\.\d+)?$");
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(this string inputData)
        {
            return Regex.IsMatch(inputData, "^[+-]?[0-9]+$");
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(this string inputData)
        {
            return Regex.IsMatch(inputData, "^[0-9]+[.]?[0-9]+$");
        }

        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(this string inputData)
        {
            return Regex.IsMatch(inputData, "^[+-]?[0-9]+[.]?[0-9]+$");
        }

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(this string inputData)
        {
            return Regex.IsMatch(inputData, "[\u4e00-\u9fa5]");
        }

        /// <summary> 
        /// 检测含有中文字符串的实际长度 
        /// </summary> 
        /// <param name="str">字符串</param> 
        public static int GetCHZNLength(this string inputData)
        {
            var n = new ASCIIEncoding();

            var bytes = n.GetBytes(inputData);

            var length = 0; // l 为字符串之实际长度 
            for (var i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63) //判断是否为汉字或全脚符号 
                {
                    length++;
                }
                length++;
            }
            return length;

        }

        /// <summary>
        /// 验证身份证是否合法 15 和 18 位两种
        /// </summary>
        /// <param name="idCard">要验证的身份证</param>
        public static bool IsIdCard(this string idCard)
        {
            if (System.String.IsNullOrEmpty(idCard))
            {
                return false;
            }

            if (idCard.Length == 15)
            {
                return Regex.IsMatch(idCard, @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$");
            }
            else if (idCard.Length == 18)
            {
                return Regex.IsMatch(idCard, @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$", RegexOptions.IgnoreCase);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是邮件地址
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(this string inputData)
        {
            return Regex.IsMatch(inputData, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        /// <summary>
        /// 邮编有效性
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static bool IsValidZip(this string zip)
        {
            return Regex.IsMatch(zip, @"^\d{6}$", RegexOptions.None);
        }

        /// <summary>
        /// 固定电话有效性
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsValidPhone(this string phone)
        {
            return Regex.IsMatch(phone, @"^(\(\d{3, 4}\)|\d{3, 4}-)?\d{7, 8}$", RegexOptions.None);
        }

        /// <summary>
        /// 手机有效性
        /// </summary>
        /// <param name="strMobile"></param>
        /// <returns></returns>
        public static bool IsValidMobile(this string mobile)
        {
            return Regex.IsMatch(mobile, @"^(13|15|17|18|19)\d{9}$", RegexOptions.None);
        }

        /// <summary>
        /// 电话有效性（固话和手机 ）
        /// </summary>
        /// <param name="strVla"></param>
        /// <returns></returns>
        public static bool IsValidPhoneAndMobile(this string number)
        {
            return Regex.IsMatch(number, @"^(\(\d{3, 4}\)|\d{3, 4}-)?\d{7, 8}$|^(13|15)\d{9}$", RegexOptions.None);
        }

        /// <summary>
        /// Url 有效性
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public bool IsValidURL(this string url)
        {
            return Regex.IsMatch(url, @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2, 3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\, \'/\\\+&%\$#\=~])*[^\.\, \)\(\s]$");
        }

        /// <summary>
        /// IP 有效性
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsValidIP(this string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// domain 有效性
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(this string host)
        {
            return host.IndexOf(".") == -1 ? false : !Regex.IsMatch(host.Replace(".", System.String.Empty), @"^\d+$");
        }

        /// <summary>
        /// 判断是否为 base64 字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(this string str)
        {
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 验证字符串是否是 GUID
        /// </summary>
        /// <param name="guid">字符串</param>
        /// <returns></returns>
        public static bool IsGuid(this string guid)
        {
            return System.String.IsNullOrEmpty(guid) ? false : Regex.IsMatch(guid, "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断输入的字符是否为日期
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsDate(this string strValue)
        {
            return Regex.IsMatch(strValue, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }

        /// <summary>
        /// 判断输入的字符是否为日期, 如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool IsDateHourMinute(this string strValue)
        {
            return Regex.IsMatch(strValue, @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }

        /// <summary>
        /// 检查字符串最大长度, 返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string CheckMathLength(this string inputData, int maxLength)
        {
            if (inputData != null && inputData != System.String.Empty)
            {
                inputData = inputData.Trim();
                if (inputData.Length > maxLength)//按最大长度截取字符串
                {
                    inputData = inputData.Substring(0, maxLength);
                }
            }
            return inputData;
        }

        /// <summary>
        /// 转换成 html 编码
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Encode(this string str)
        {
            return str
                .Replace("&", "&amp;")
                .Replace("'", "''")
                .Replace("\"", "&quot;")
                .Replace(" ", "&nbsp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\n", "<br>");

        }

        /// <summary>
        /// 解析 html 成普通文本
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string</returns>
        public static string Decode(this string str)
        {
            return str
                .Replace("<br>", "\n")
                .Replace("&gt;", ">")
                .Replace("&lt;", "<")
                .Replace("&nbsp;", " ")
                .Replace("&quot;", "\"");

        }
    }
}
