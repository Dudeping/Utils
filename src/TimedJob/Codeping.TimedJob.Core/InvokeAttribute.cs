using System;

namespace Codeping.TimedJob.Core
{
    public class InvokeAttribute : Attribute
    {
        public bool IsEnabled { get; set; } = true;

        public DateTime Begin { get; set; }

        public long Interval { get; set; } = 1000 * 60 * 60 * 24; // 24 hours

        public bool SkipWhileExecuting { get; set; } = false;
    }
}
