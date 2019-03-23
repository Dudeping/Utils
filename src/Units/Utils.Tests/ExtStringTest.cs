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

        [Fact]
        public void FirstLowerCaseTest()
        {
            Assert.Equal("codeping", "Codeping".FirstLowerCase());

            Assert.Equal("uTILS", "UTILS".FirstLowerCase());
        }

        [Fact]
        public void FirstUpperCaseTest()
        {
            Assert.Equal("Codeping", "codeping".FirstUpperCase());

            Assert.Equal("CODEPING", "cODEPING".FirstUpperCase());
        }

        [Fact]
        public void RemoveEndTest()
        {
            Assert.Equal("code", "codeping".RemoveEnd("ping"));

            Assert.Equal("", "   ".RemoveEnd(" "));

            Assert.Equal("util", "utils".RemoveEnd("s"));
        }
    }
}
