using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Codeping.Utils
{
    public static class MethodInfoExtensions
    {
        public static string GetFullName(this MethodInfo method)
        {
            return $"{method.DeclaringType.FullName}.{method.Name}";
        }
    }
}
