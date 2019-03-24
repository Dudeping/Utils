using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Tests
{
    internal class Child : Parent, IChild
    {
        public bool Sex { get; set; }

        public void Run()
        {
        }
    }
}
