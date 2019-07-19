using System;
using Codeping.Clrex;

namespace Codeping.Clrex.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicTest();

            Console.ReadKey();
        }

        static void BasicTest()
        {
            ConsoleEx.CoverCurrentLine(8, ConsoleColor.DarkYellow);

            ConsoleEx.WriteLine("6000005555555555066", ConsoleColor.DarkRed);
            ConsoleEx.WriteLine("6000005555555555066", ConsoleColor.DarkRed);

            ConsoleEx.CoverCurrentLine(80, ConsoleColor.DarkGreen);

            Console.Write(Console.WindowWidth);
        }

        static void MuliTest()
        {

        }

        static void ProgressTest()
        {
        }
    }
}
