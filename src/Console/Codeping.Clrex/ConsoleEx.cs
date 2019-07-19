using System;
using System.Drawing;

namespace Codeping.Clrex
{
    public class ConsoleEx
    {
        /// <summary>
        /// 使用指定的颜色输出数据, 输出完成后会将颜色复位
        /// </summary>
        /// <param name="data">要输出的数据</param>
        /// <param name="color">指定的文字颜色</param>
        public static void Write(object data, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.Write(data);

            Console.ResetColor();
        }

        /// <summary>
        /// 使用指定的颜色输出数据, 并在末尾输出换行符, 输出完成后会将颜色复位
        /// </summary>
        /// <param name="data">要输出的数据</param>
        /// <param name="color">指定的文字颜色</param>
        public static void WriteLine(object data, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(data);

            Console.ResetColor();
        }

        /// <summary>
        /// 使用指定的颜色输出数据, 输出完成后会将颜色复位
        /// </summary>
        /// <param name="data">要输出的数据</param>
        /// <param name="foregroundColor">指定的文字颜色</param>
        /// <param name="backgroundColor">指定的背景颜色</param>
        public static void Write(
            object data,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.Write(data);

            Console.ResetColor();
        }

        /// <summary>
        /// 使用指定的颜色输出数据, 并在末尾输出换行符, 输出完成后会将颜色复位
        /// </summary>
        /// <param name="data">要输出的数据</param>
        /// <param name="foregroundColor">指定的文字颜色</param>
        /// <param name="backgroundColor">指定的背景颜色</param>
        public static void WriteLine(
            object data,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(data);

            Console.ResetColor();
        }

        /// <summary>
        /// 覆盖当前行, 执行输出操作
        /// </summary>
        /// <param name="write">输出操作</param>
        public static void CoverCurrentLine(Action write)
        {
            MoveCursorPosition();

            write?.Invoke();
        }

        /// <summary>
        /// 覆盖当前行, 然后使用指定的颜色输出数据, 并在末尾输出换行符, 输出完成后会将颜色复位
        /// </summary>
        /// <param name="data">要输出的数据</param>
        /// <param name="color">指定的文字颜色</param>
        public static void CoverCurrentLine(object data, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            MoveCursorPosition();

            Console.WriteLine(data);

            Console.ResetColor();
        }

        /// <summary>
        /// 覆盖当前行, 然后使用指定的颜色输出数据, 并在末尾输出换行符, 输出完成后会将颜色复位
        /// </summary>
        /// <param name="data">要输出的数据</param>
        /// <param name="foregroundColor">指定的文字颜色</param>
        /// <param name="backgroundColor">指定的背景颜色</param>
        public static void CoverCurrentLine(
            object data,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(data);

            MoveCursorPosition();

            Console.ResetColor();
        }

        /// <summary>
        /// 进度输出, 返回一个可继续输出的接口
        /// </summary>
        /// <param name="message">开始信息</param>
        /// <param name="total">总量</param>
        /// <param name="style">进度条样式, 可以自定义</param>
        /// <returns></returns>
        public static IProgressor WriteProgress(string message, int total, string style = PrintStyle.Arrow)
        {
            return new Progressor(message, total, style);
        }

        /// <summary>
        /// 移动光标并覆盖掉光标之后的内容
        /// </summary>
        private static void MoveCursorPosition()
        {
            var current = new Point(Console.CursorLeft, Console.CursorTop);

            var row = Console.CursorLeft == 0 && Console.CursorTop > 0
                ? Console.CursorTop - 1 : Console.CursorTop;

            Console.SetCursorPosition(0, row);

            while (Console.CursorLeft <= current.X
                || Console.CursorTop <= current.Y)
            {
                Console.Write('\0');
            }

            Console.SetCursorPosition(0, row);
        }
    }
}
