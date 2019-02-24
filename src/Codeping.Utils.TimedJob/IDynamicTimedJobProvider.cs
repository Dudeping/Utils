using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.Utils.TimedJob
{
    public interface IDynamicTimedJobProvider
    {
        IList<DynamicTimedJob> GetJobs();
    }
}
