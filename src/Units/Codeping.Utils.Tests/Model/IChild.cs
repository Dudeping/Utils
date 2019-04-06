using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Tests
{
    internal interface IChild : IParent
    {
        bool Sex { get; }
        void Run();
    }
}
