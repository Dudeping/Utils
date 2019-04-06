using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExtFormatTest
    {
        [Fact]
        public void DescriptionTest()
        {
            Assert.Equal("是", true.Description());

            Assert.Equal("否", false.Description());

            Assert.Empty(((bool?)null).Description());
        }

        [Fact]
        public void RmbTest()
        {
            Assert.Equal("￥1", ((decimal)1).Rmb());

            Assert.Equal("￥1.1", ((decimal)1.1).Rmb());

            Assert.Equal("￥1.22", ((decimal)1.222).Rmb());
        }

        [Fact]
        public void PercentTest()
        {
            Assert.Equal("1%", ((decimal)1).Percent());

            Assert.Equal("1.1%", ((decimal)1.1).Percent());

            Assert.Equal("1.22%", ((decimal)1.222).Percent());
        }
    }
}
