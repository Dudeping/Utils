using Codeping.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utils.Tests
{
    public class CheckTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NotNullTest(object data)
        {
            if (data.SafeString().IsEmpty())
            {
                try
                {
                    this.NotNull(data);

                    Assert.True(false, "null 校验失败!");
                }
                catch (ArgumentNullException)
                {
                    Assert.True(true, "null 校验成功!");
                }
            }
            else
            {
                this.NotEmpty(data);
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("a")]
        public void NotEmptyTest(object data)
        {
            if (data.SafeString().IsEmpty())
            {
                try
                {
                    this.NotEmpty(data);

                    Assert.True(false, "Empty 校验失败!");
                }
                catch (ArgumentNullException)
                {
                    Assert.True(true, "Empty 校验成功!");
                }
            }
            else
            {
                this.NotEmpty(data);
            }
        }

        private void NotNull([NotNull] object obj)
        {
        }

        private void NotEmpty([NotEmpty] object obj)
        {
        }
    }
}
