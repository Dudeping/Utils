using Codeping.Utils.TimedJob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TimedJobTests
{
    public class Writer : IJob
    {
        [Invoke(Interval = 2 * 1000, IsEnabled = true, SkipWhileExecuting = true)]
        public void Print()
        {
            Debug.WriteLine(DateTime.Now);
        }
    }
}
