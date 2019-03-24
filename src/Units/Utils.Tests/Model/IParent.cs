using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Tests
{
    internal interface IParent
    {
        int Id { get; }
        string Name { get; }
        Status Status { get; }
        void Eat();
    }
}
