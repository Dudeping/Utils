using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Codeping.Utils.TimedJob
{
    public interface IAssemblyLocator
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Might be expensive.")]
        IList<Assembly> GetAssemblies();
    }
}
