using Codeping.Utils.TimedJob;
using System;

namespace TimedJob.Core.Demo
{
    public class Writer : IJob
    {
        [Invoke(Interval = 2 * 1000, IsEnabled = true, SkipWhileExecuting = true)]
        public void Print()
        {
            Console.WriteLine($"Print: {DateTime.Now}");
        }
    }
}
