using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExRandomTest
    {
        [Fact]
        public void Generate()
        {
            Assert.Empty(RandomEx.GenerateString(0));
            Assert.NotEmpty(RandomEx.GenerateString(5));

            Assert.NotEmpty(RandomEx.GenerateGuid());
            Assert.IsType<bool>(RandomEx.GenerateBool());

            Assert.NotEqual(0, RandomEx.GenerateInt(5));
            Assert.NotEqual(default, RandomEx.GenerateDate());

            Assert.NotEmpty(RandomEx.GenerateLetters(5));
            Assert.NotEmpty(RandomEx.GenerateNumbers(5));
            Assert.NotEmpty(RandomEx.GenerateChinese(5));

            Assert.NotEqual(0, RandomEx.GenerateInt(1, 5));
            Assert.IsType<Status>(RandomEx.GenerateEnum<Status>());
        }
    }
}
