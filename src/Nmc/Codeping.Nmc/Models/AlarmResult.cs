using System.Collections.Generic;

namespace Codeping.Nmc
{
    public class AlarmResult
    {
        public int Total { get; set; }
        public IEnumerable<AlarmModel> Items { get; set; }
    }
}
