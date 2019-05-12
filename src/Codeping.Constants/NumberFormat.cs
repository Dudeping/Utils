namespace Codeping.Constants
{
    /// <summary>
    /// 数字格式化参数
    /// 所有的字母参数均可以和数字组合, 数字叫作精度说明符, 一般表示保留多少位, 或者补齐多少位;
    /// 如: 25.ToString("D4") -> 0025;
    /// 如: 25.ToString("F2") -> 25.00;
    /// 如: string.Format("{0:P2}", 0.25) -> 25.00%;
    /// </summary>
    public class F_Num
    {
        /// <summary>
        /// 货币, 如: 2.5.ToString("C") -> ￥2.50
        /// </summary>
        public const string C = nameof(C);

        /// <summary>
        /// 占位, 如: 25.ToString("D4") -> 0025
        /// </summary>
        public const string D = nameof(D);

        /// <summary>
        /// 科学计数法, 如: 2500.ToString("E") -> 2.500000E+003
        /// 如果不加数字, 默认 6 位小数
        /// </summary>
        public const string E = nameof(E);

        /// <summary>
        /// 科学计数法, 如: 2500.ToString("e") -> 2.5e+003
        /// </summary>
        public const string e = nameof(e);

        /// <summary>
        /// 小数点, 如: 25.ToString("F2") -> 25.00
        /// </summary>
        public const string F = nameof(F);

        /// <summary>
        /// 加分隔符, 如: 4384.5.ToString("N") -> 4,384.50
        /// </summary>
        public const string N = nameof(N);

        /// <summary>
        /// 十六进制, 如: 225.ToString("X") -> 1120
        /// </summary>
        public const string X = nameof(X);

        /// <summary>
        /// 百分比, 如: 0.223.ToString("P") -> 22.3%
        /// </summary>
        public const string P = nameof(P);

        /// <summary>
        /// 自定义占位符, 没有会以 0 补齐
        /// 如: 0.025.ToString("00.00%") -> 02.50%
        /// </summary>
        public const string C0 = "0";

        /// <summary>
        /// 自定义占位符, 没有不会补齐
        /// 如: 0.025.ToString("##.##%") -> 2.5%
        /// </summary>
        public const string CS = "#";

        /// <summary>
        /// 自定义百分比
        /// 如: 0.025.ToString("00.00%") -> 02.50%
        /// </summary>
        public const string CP = "%";

        /// <summary>
        /// 自定义小数点
        /// 如: 0.025.ToString("00.00%") -> 02.50%
        /// </summary>
        public const string CD = ".";
    }
}
