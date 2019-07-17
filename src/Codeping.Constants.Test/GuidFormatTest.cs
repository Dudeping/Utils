using System;
using Xunit;

namespace Codeping.Constants.Tests
{
    public class GuidFormatTest
    {
        [Fact]
        public void GuidNTest()
        {
            var guid = Guid.NewGuid()
                .ToString(F_Guid.N);

            Assert.NotNull(guid);
            Assert.NotEmpty(guid);
            Assert.Equal(32, guid.Length);

            Assert.False(guid.StartsWith("{"));
            Assert.False(guid.EndsWith("}"));
            Assert.DoesNotContain("-", guid);
        }

        [Fact]
        public void GuidDTest()
        {
            var guid = Guid.NewGuid()
                .ToString(F_Guid.D);

            Assert.NotNull(guid);
            Assert.NotEmpty(guid);
            Assert.Equal(36, guid.Length);

            Assert.False(guid.StartsWith("{"));
            Assert.False(guid.EndsWith("}"));
            Assert.Contains("-", guid);
        }

        [Fact]
        public void GuidBTest()
        {
            var guid = Guid.NewGuid()
                .ToString(F_Guid.B);

            Assert.NotNull(guid);
            Assert.NotEmpty(guid);
            Assert.Equal(38, guid.Length);

            Assert.StartsWith("{", guid);
            Assert.EndsWith("}", guid);
            Assert.Contains("-", guid);
        }

        [Fact]
        public void GuidPTest()
        {
            var guid = Guid.NewGuid()
                .ToString(F_Guid.P);

            Assert.NotNull(guid);
            Assert.NotEmpty(guid);
            Assert.Equal(38, guid.Length);

            Assert.StartsWith("(", guid);
            Assert.EndsWith(")", guid);
            Assert.Contains("-", guid);
        }

        [Fact]
        public void GuidXTest()
        {
            var guid = Guid.NewGuid()
                .ToString(F_Guid.X);

            Assert.NotNull(guid);
            Assert.NotEmpty(guid);
            Assert.Equal(68, guid.Length);

            Assert.StartsWith("{", guid);
            Assert.EndsWith("}}", guid);
            Assert.Contains(",", guid);
            Assert.Contains("0x", guid);
        }
    }
}
