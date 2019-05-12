namespace Codeping.Constants
{
    /// <summary>
    /// GUID 格式化参数
    /// 注意: 所有格式化后的字符串均为小写, 如需转大写需要自行处理, 如: .ToUpper();
    /// </summary>
    public class F_Guid
    {
        /// <summary>
        /// 无括号, 无连字符, 如: 102447989a344245b1ef9143f9b1e68a
        /// </summary>
        public const string N = nameof(N);

        /// <summary>
        /// 无括号, 有连字符, 如: 10244798-9a34-4245-b1ef-9143f9b1e68a
        /// </summary>
        public const string D = nameof(D);

        /// <summary>
        /// 花括号, 连字符, 如: {10244798-9a34-4245-b1ef-9143f9b1e68a}
        /// </summary>
        public const string B = nameof(B);

        /// <summary>
        /// 小括号, 连字符, 如: (10244798-9a34-4245-b1ef-9143f9b1e68a)
        /// </summary>
        public const string P = nameof(P);

        /// <summary>
        /// 六进制, 如: {0x10244798,0x9a34,0x4245,{0xb1,0xef,0x91,0x43,0xf9,0xb1,0xe6,0x8a}}
        /// </summary>
        public const string X = nameof(X);
    }
}
