using System;
using System.Collections.Generic;
using System.Text;
using Codeping.Utils;
using Xunit;

namespace Utils.Tests
{
    public class ExtStringTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("zz", "智障")]
        [InlineData("jsj", "计算机")]
        [InlineData("js", "技术")]
        [InlineData("tl", "图灵")]
        [InlineData("sjhp", "世界和平")]
        [InlineData("dtnt", "道特内特")]
        public void PinYinTest(string expected, string data)
        {
            Assert.Equal(expected, data.PinYin());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("codeping", "Codeping")]
        [InlineData("uTILS", "UTILS")]
        [InlineData("技术", "技术")]
        public void FirstLowerCaseTest(string expected, string data)
        {
            Assert.Equal(expected, data.FirstLowerCase());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Codeping", "codeping")]
        [InlineData("CODEPING", "cODEPING")]
        [InlineData("技术", "技术")]
        public void FirstUpperCaseTest(string expected, string data)
        {
            Assert.Equal(expected, data.FirstUpperCase());
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("code", "codeping", "ping")]
        [InlineData("", "   ", " ")]
        [InlineData("util", "utils", "s")]
        [InlineData("lalala", "lalala", "")]
        [InlineData("lalala", "lalala", "s")]
        public void RemoveEndTest(string expected, string data, string remove)
        {
            Assert.Equal(expected, data.RemoveEnd(remove));
        }
    }
}
