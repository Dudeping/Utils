using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Codeping.Utils
{
    /// <summary>
    /// 随机操作
    /// </summary>
    public static class RandomEx
    {
        private static readonly Random _rnd = new Random(Environment.TickCount);

        /// <summary>
        /// 生成 Guid
        /// </summary>
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 生成固定长度随机字符串
        /// </summary>
        /// <param name="maxLength">长度</param>
        /// <param name="text">如果传入该参数, 则从该文本中随机抽取</param>
        public static string GenerateString(long length, string text = null)
        {
            if (text == null)
            {
                text = Const.Letters + Const.Letters.ToUpper() + Const.Numbers;
            }

            StringBuilder result = new StringBuilder();

            for (long i = 0; i < length; i++)
            {
                result.Append(text[_rnd.Next(1, text.Length)].ToString());
            }

            return result.ToString();
        }

        /// <summary>
        /// 生成随机字母
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public static string GenerateLetters(int maxLength)
        {
            return RandomEx.GenerateString(maxLength, Const.Letters);
        }

        /// <summary>
        /// 生成随机汉字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public static string GenerateChinese(int maxLength)
        {
            return RandomEx.GenerateString(maxLength, Const.SimplifiedChinese);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public static string GenerateNumbers(int maxLength)
        {
            return RandomEx.GenerateString(maxLength, Const.Numbers);
        }

        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        public static bool GenerateBool()
        {
            int random = _rnd.Next(1, 100);

            if (random % 2 == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 生成随机整数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public static int GenerateInt(int maxValue)
        {
            return _rnd.Next(0, maxValue + 1);
        }

        /// <summary>
        /// 生成随机整数
        /// </summary>
        /// <param name="maxValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public static int GenerateInt(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="beginYear">起始年份</param>
        /// <param name="endYear">结束年份</param>
        public static DateTime GenerateDate(int beginYear = 1980, int endYear = 2080)
        {
            int year = _rnd.Next(beginYear, endYear);
            int month = _rnd.Next(1, 13);
            int day = _rnd.Next(1, 29);
            int hour = _rnd.Next(1, 24);
            int minute = _rnd.Next(1, 60);
            int second = _rnd.Next(1, 60);

            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 生成随机枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public static TEnum GenerateEnum<TEnum>() where TEnum : Enum
        {
            List<Item> items = EnumEx.GetItems<TEnum>().ToList<Item>();

            int index = _rnd.Next(0, items.Count);

            return EnumEx.Parse<TEnum>(items[index].Value);
        }
    }
}
