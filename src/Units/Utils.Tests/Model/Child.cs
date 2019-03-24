using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utils.Tests
{
    internal class Child : Parent, IChild
    {
        [DisplayName("性别")]
        [Description("性别")]
        public bool Sex { get; set; }

        public void Run()
        {
        }
    }
}
