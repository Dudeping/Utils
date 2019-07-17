namespace Codeping.Constants
{
    public partial class F_Date
    {
        /// <summary>
        /// 组合参数: 月中的某一天, 一位数的日期没有前导零
        /// </summary>
        public const string d = nameof(d);

        /// <summary>
        /// 组合参数: 月中的某一天, 一位数的日期有一个前导零
        /// </summary>
        public const string dd = nameof(dd);

        /// <summary>
        /// 组合参数: 周中某天的缩写名称 <see cref="AbbreviatedDayNames"/>
        /// </summary>
        public const string ddd = nameof(ddd);

        /// <summary>
        /// 组合参数: 周中某天的完整名称 <see cref="DayNames"/>
        /// </summary>
        public const string dddd = nameof(dddd);

        /// <summary>
        /// 组合参数: 月份数字, 一位数的月份没有前导零
        /// </summary>
        public const string M = nameof(M);

        /// <summary>
        /// 组合参数: 月份数字, 一位数的月份有前导零
        /// </summary>
        public const string MM = nameof(MM);

        /// <summary>
        /// 组合参数: 月份的缩写名称 <see cref="AbbreviatedMonthNames"/>
        /// </summary>
        public const string MMM = nameof(MMM);

        /// <summary>
        /// 组合参数: 月份的完整名称 <see cref="MonthNames"/>
        /// </summary>
        public const string MMMM = nameof(MMMM);

        /// <summary>
        /// 组合参数: 不包含纪元的年份, 如果不包含纪元的年份小于 10, 则显示不具有前导零的年份
        /// </summary>
        public const string y = nameof(y);

        /// <summary>
        /// 组合参数: 不包含纪元的年份, 如果不包含纪元的年份小于 10, 则显示具有前导零的年份
        /// </summary>
        public const string yy = nameof(yy);

        /// <summary>
        /// 组合参数: 包括纪元的四位数的年份
        /// </summary>
        public const string yyyy = nameof(yyyy);

        /// <summary>
        /// 组合参数: 小时制的小时, 一位数的小时数没有前导零
        /// </summary>
        public const string h = nameof(h);

        /// <summary>
        /// 组合参数: 12 小时制的小时, 一位数的小时数有前导零
        /// </summary>
        public const string hh = nameof(hh);

        /// <summary>
        /// 组合参数: 24 小时制的小时, 一位数的小时数没有前导零
        /// </summary>
        public const string H = nameof(H);

        /// <summary>
        /// 组合参数: 24 小时制的小时, 一位数的小时数有前导零
        /// </summary>
        public const string HH = nameof(HH);

        /// <summary>
        /// 组合参数: 分钟, 一位数的分钟数没有前导零
        /// </summary>
        public const string m = nameof(m);

        /// <summary>
        /// 组合参数: 分钟, 一位数的分钟数有一个前导零
        /// </summary>
        public const string mm = nameof(mm);

        /// <summary>
        /// 组合参数: 秒, 一位数的秒数没有前导零
        /// </summary>
        public const string s = nameof(s);

        /// <summary>
        /// 组合参数: 秒, 一位数的秒数有前导零
        /// </summary>
        public const string ss = nameof(ss);

        /// <summary>
        /// 组合参数: 秒的小数精度为一位, 其余数字被截断
        /// </summary>
        public const string f = nameof(f);

        /// <summary>
        /// 组合参数: 秒的小数精度为两位, 其余数字被截断
        /// </summary>
        public const string ff = nameof(ff);

        /// <summary>
        /// 组合参数: 秒的小数精度为三位, 其余数字被截断
        /// </summary>
        public const string fff = nameof(fff);

        /// <summary>
        /// 组合参数: 秒的小数精度为四位, 其余数字被截断
        /// </summary>
        public const string ffff = nameof(ffff);

        /// <summary>
        /// 组合参数: 秒的小数精度为五位, 其余数字被截断
        /// </summary>
        public const string fffff = nameof(fffff);

        /// <summary>
        /// 组合参数: 秒的小数精度为六位, 其余数字被截断
        /// </summary>
        public const string ffffff = nameof(ffffff);

        /// <summary>
        /// 组合参数: 秒的小数精度为七位, 其余数字被截断
        /// </summary>
        public const string fffffff = nameof(fffffff);

        /// <summary>
        /// 组合参数: AM/PM 指示项的第一个字符
        /// </summary>
        public const string t = nameof(t);

        /// <summary>
        /// 组合参数: AM/PM 指示项
        /// </summary>
        public const string tt = nameof(tt);

        /// <summary>
        /// 组合参数: 一位数的小时数没有前导零, 例如: 太平洋标准时间是 "-8"
        /// </summary>
        public const string z = nameof(z);

        /// <summary>
        /// 组合参数: 一位数的小时数有前导零, 例如: 太平洋标准时间是 "-08"
        /// </summary>
        public const string zz = nameof(zz);

        /// <summary>
        /// 组合参数: 一位数的小时数和分钟数有前导零, 例如: 太平洋标准时间是 "-08:00"
        /// </summary>
        public const string zzz = nameof(zzz);

        /// <summary>
        /// 默认时间分隔符
        /// </summary>
        public const string TimeSeparator = ":";

        /// <summary>
        /// 默认日期分隔符
        /// </summary>
        public const string DateSeparator = "/";
    }
}
