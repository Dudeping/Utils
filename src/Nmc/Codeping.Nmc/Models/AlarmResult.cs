using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils.Nmc
{
    public class AlarmResult
    {
        public int Total { get; set; }
        public IEnumerable<AlarmModel> Items { get; set; }
    }
}
