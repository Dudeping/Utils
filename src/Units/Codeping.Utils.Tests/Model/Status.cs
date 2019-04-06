using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utils.Tests
{
    internal enum Status
    {
        [Description("婴儿")]
        Baby,
        [Description("少年")]
        Young,
        [Description("成年")]
        Adult,
        [Description("老年")]
        Old,
    }
}
