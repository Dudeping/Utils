using System;

namespace Codeping.Utils
{
    public interface IProgressor
    {
        /// <summary>
        /// 输出进度
        /// </summary>
        /// <param name="rate">当前进度</param>
        void Progress(int rate);
        /// <summary>
        /// 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        /// </summary>
        /// <param name="message">要输出的文字</param>
        /// <param name="color">指定的文字颜色</param>
        void Finished(string message, ConsoleColor color = ConsoleColor.White);
        /// <summary>
        /// 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        /// </summary>
        /// <param name="message">要输出的文字</param>
        /// <param name="foregroundColor">指定的文字颜色</param>
        /// <param name="backgroundColor">指定的背景颜色</param>
        void Finished(
            string message,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black);
    }
}
