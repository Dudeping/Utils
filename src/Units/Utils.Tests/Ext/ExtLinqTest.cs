using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExtLinqTest
    {
        [Fact]
        public void JoinTest()
        {
            Assert.Equal("a,b", new[] { "a", "b" }.Join());

            Assert.Equal("'a','b'", new[] { "a", "b" }.Join(quotes: "'"));

            Assert.Equal("a|b", new[] { "a", "b" }.Join("|"));

            Assert.Equal("`a`@`b`", new[] { "a", "b" }.Join("@", "`"));
        }
    }
}
