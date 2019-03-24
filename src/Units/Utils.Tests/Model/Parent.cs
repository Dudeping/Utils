using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Tests
{
    internal class Parent : IParent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public void Eat()
        {
        }
    }
}
