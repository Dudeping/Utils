using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;
using System.IO;
using System.Threading.Tasks;

namespace Utils.Tests
{
    public class ExtStreamTest
    {
        [Fact]
        public async Task StreamTestAsync()
        {
            using (var ms = new MemoryStream("hello".ToBytes()))
            {
                Assert.Equal("hello", ms.ToText(isCloseStream: false));
                Assert.Equal("hello", ms.CopyToText(isCloseStream: false));
                Assert.Equal("hello", await ms.ToTextAsync(isCloseStream: false));
                Assert.Equal("hello", await ms.CopyToTextAsync(isCloseStream: false));
            }

            Stream stream = null;

            Assert.Equal("", stream.ToText());
            Assert.Equal("", await stream.ToTextAsync());
            Assert.Equal("", stream.CopyToText());
            Assert.Equal("", await stream.CopyToTextAsync());
        }
    }
}
