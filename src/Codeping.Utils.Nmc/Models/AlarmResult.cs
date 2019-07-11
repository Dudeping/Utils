using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils.Nmc
{
    public class AlarmResult
    {
        public int Count { get; set; }
        public int Total { get; set; }
        public IAsyncEnumerable<AlarmModel> Items { get; set; }
    }
}
