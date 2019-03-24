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
            Assert.Equal("Baby", Status.Baby.Name());

            Assert.Equal("Adult", Status.Adult.Name());
        }

        [Fact]
        public void ValueTest()
        {
            Assert.Equal(0, Status.Baby.Value());

            Assert.Equal(1, Status.Young.Value());
        }

        [Fact]
        public void DescriptionTest()
        {
            Assert.Equal("婴儿", Status.Baby.Description());

            Assert.Equal("成年", Status.Adult.Description());
        }
    }
}
