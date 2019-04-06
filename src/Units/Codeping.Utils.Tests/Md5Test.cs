using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class Md5Test
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("世界和平", "FE918912DEB8DD45")]
        [InlineData("codeping.utils", "46DB5418615534B6")]
        public void Md5By16Test(string text, string md5)
        {
            Assert.Equal(md5, text.Md5By16());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("世界和平", "B38B2399FE918912DEB8DD452DB6F776")]
        [InlineData("codeping.utils", "0259422446DB5418615534B61A0ECA14")]
        public void Md5By32Test(string text, string md5)
        {
            Assert.Equal(md5, text.Md5By32());
        }
    }
}
