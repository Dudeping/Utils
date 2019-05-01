using System;

namespace Codeping.Utils
{
    public class ConsoleEx
    {
        // 使用指定的颜色输出数据, 输入完成后会将颜色复位
        public static void Write(object data, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.Write(data);

            Console.ResetColor();
        }

        // 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        public static void WriteLine(object data, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(data);

            Console.ResetColor();
        }

        // 使用指定的颜色输出数据, 输入完成后会将颜色复位
        public static void Write(object data, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            Console.Write(data);

            Console.ResetColor();
        }

        // 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        public static void WriteLine(object data, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            Console.WriteLine(data);

            Console.ResetColor();
        }

        // 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        public static void CoverCurrentLine(Action write)
        {
            MoveCursorPosition();

            write?.Invoke();
        }

        // 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        public static void CoverCurrentLine(object data, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            MoveCursorPosition();

            Console.WriteLine(data);

            Console.ResetColor();
        }

        // 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        public static void CoverCurrentLine(object data, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(data);

            MoveCursorPosition();

            Console.ResetColor();
        }

        // 进度输出, 返回一个可继续写的接口
        public static IProgressor StartProgress(string message)
        {
            return new Progressor(message);
        }

        private static void MoveCursorPosition()
        {
            if (Console.CursorLeft == 0)
            {
                Console.SetCursorPosition(
                    Console.CursorLeft, Console.CursorTop - 1);
            }
            else
            {
                Console.SetCursorPosition(
                    0, Console.CursorTop);
            }
        }
    }

    internal class Progressor : IProgressor
    {
        private readonly string _message;

        public Progressor(string message)
        {
            _message = message;
        }

        // 输出完成文字
        public void Finished(string message, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleEx.CoverCurrentLine(message, color);
        }

        // 输出完成文字
        public void Finished(string message, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            ConsoleEx.CoverCurrentLine(message, foregroundColor, backgroundColor);
        }

        // 输出进度
        public void Progress(int num, int total)
        {
        }
    }

    public interface IProgressor
    {
        // 输出进度
        void Progress(int num, int total);
        // 输出完成文字
        void Finished(string message, ConsoleColor color = ConsoleColor.White);
        void Finished(
            string message,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black);
    }
}
