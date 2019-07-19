using System;

namespace Codeping.Utils
{
    internal class Progressor : IProgressor
    {
        private readonly int _total;
        private readonly string _style;
        private readonly string _message;

        public Progressor(
            string message,
            int total,
            string style = PrintStyle.Arrow)
        {
            _total = total;
            _style = this.HandleStyle(style);
            _message = string.IsNullOrWhiteSpace(message) ? "..." : message;

            this.PrintStart();
        }

        /// <summary>
        /// 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        /// </summary>
        /// <param name="message">要输出的文字</param>
        /// <param name="color">指定的文字颜色</param>
        public void Finished(string message, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleEx.CoverCurrentLine(message, color);
        }

        /// <summary>
        /// 使用指定的颜色输出数据, 并在输入完成后输入换行符, 输入完成后会将颜色复位
        /// </summary>
        /// <param name="message">要输出的文字</param>
        /// <param name="foregroundColor">指定的文字颜色</param>
        /// <param name="backgroundColor">指定的背景颜色</param>
        public void Finished(
            string message,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            ConsoleEx.CoverCurrentLine(message, foregroundColor, backgroundColor);
        }

        /// <summary>
        /// 输出进度
        /// </summary>
        /// <param name="rate">当前进度</param>
        public void Progress(int rate)
        {
            ConsoleEx.CoverCurrentLine(() =>
            {
                Console.Write(_message);

                var p = (double)rate / _total;

                p.ToString("P2");

                if (rate < _total)
                {
                    var total = Console.WindowWidth
                        - 2 // 括号
                        - 6 // 百分比
                        - _message.Length
                        - (_style.Length - 1);
                }



                Console.Write('[');

                Console.Write(']');
            });
        }

        private void PrintStart()
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }

            Console.Write(_message);
        }

        private string HandleStyle(string style)
        {
            if (string.IsNullOrWhiteSpace(style))
            {
                style = "#";
            }

            return style.Length > 2
                ? style.Substring(style.Length - 2)
                : style;
        }
    }
}
