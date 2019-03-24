using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;
using System.ComponentModel;

namespace Utils.Tests
{
    public class ExtEnumTest
    {
        [Fact]
        public void NameTest()
        {
            Assert.Equal("A", EnumTest.A.Name());

            Assert.Equal("B", EnumTest.B.Name());
        }

        [Fact]
        public void ValueTest()
        {
            Assert.Equal(0, EnumTest.A.Value());

            Assert.Equal(1, EnumTest.B.Value());
        }

        [Fact]
        public void DescriptionTest()
        {
            Assert.Equal("TestA", EnumTest.A.Description());

            Assert.Equal("TestB", EnumTest.B.Description());
        }
    }

    enum EnumTest
    {
        [Description("TestA")]
        A,

        [Description("TestB")]
        B,

        [Description("TestC")]
        C,
    }
}
