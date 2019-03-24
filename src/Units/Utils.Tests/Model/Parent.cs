using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Utils.Tests
{
    [Description("测试用父类")]
    internal class Parent : IParent
    {
        [Display(Name = "标识")]
        [Description("唯一标识")]
        public int Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public void Eat()
        {
        }
    }
}
