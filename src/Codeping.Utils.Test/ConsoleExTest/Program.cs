using System;
using Codeping.Utils;

namespace ConsoleExTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = DateTime.Now;

            Console.WriteLine(dt);
            Console.WriteLine(dt.ToLongDateString());
            Console.WriteLine(dt.ToFileTime());
            Console.WriteLine(dt.ToFileTimeUtc());
            Console.WriteLine(dt.ToLocalTime());
            Console.WriteLine(dt.ToLongDateString());
            Console.WriteLine(dt.ToLongTimeString());
            Console.WriteLine(dt.ToOADate());
            Console.WriteLine(dt.ToShortDateString());
            Console.WriteLine(dt.ToShortTimeString());
            Console.WriteLine(dt.ToUniversalTime());


            Console.WriteLine(0.025.ToString("00%"));

            Console.WriteLine(string.Format("{0:P2}", 0.25));

            Console.WriteLine(0.12.ToString("P2"));
            Console.WriteLine(0.122.ToString("P2"));
            Console.WriteLine(0.1221.ToString("P2"));
            Console.WriteLine(0.1220.ToString("P2"));
            Console.WriteLine(0.0225.ToString("P2"));

            Console.WriteLine("---------");

            Console.WriteLine(0.12.ToString("##.##%"));
            Console.WriteLine(0.122.ToString("##.##%"));
            Console.WriteLine(0.1221.ToString("##.##%"));
            Console.WriteLine(0.1220.ToString("##.##%"));
            Console.WriteLine(0.025.ToString("##.##%"));

            Console.WriteLine("---------");

            Console.WriteLine(0.12.ToString("00.00%"));
            Console.WriteLine(0.122.ToString("00.00%"));
            Console.WriteLine(0.1221.ToString("00.00%"));
            Console.WriteLine(0.1220.ToString("00.00%"));
            Console.WriteLine(0.025.ToString("00.00%"));

            ConsoleEx.CoverCurrentLine(8, ConsoleColor.DarkYellow);

            ConsoleEx.WriteLine("6000005555555555066", ConsoleColor.DarkRed);
            ConsoleEx.WriteLine("6000005555555555066", ConsoleColor.DarkRed);

            ConsoleEx.CoverCurrentLine(80, ConsoleColor.DarkGreen);

            Console.Write(Console.WindowWidth);

            Console.ReadKey();
        }
    }
}
