using System.Collections.Generic;
using System.Reflection;

namespace Codeping.Utils.TimedJob
{
    public interface IAssemblyLocator
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
