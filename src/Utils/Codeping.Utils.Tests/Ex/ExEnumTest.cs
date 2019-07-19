using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;
using System.Linq;

namespace Utils.Tests
{
    public class ExEnumTest
    {
        [Fact]
        public void GetItemsTest()
        {
            var items = EnumEx.GetItems<Status>();

            Assert.Equal(4, items.Count());

            Assert.Equal("婴儿,少年,成年,老年", items.Select(x => x.Text).Join());

            Assert.Equal("0,1,2,3", items.Select(x => x.Value).Join());
        }

        [Fact]
        public void ParseTest()
        {
            Assert.Equal(Status.Young, EnumEx.Parse<Status>(1));
            Assert.Equal(Status.Young, EnumEx.Parse<Status>("1"));
            Assert.Equal(Status.Young, EnumEx.Parse<Status>("Young"));
        }

        [Fact]
        public void GetNameTest()
        {
            Assert.Equal("Young", EnumEx.GetName<Status>(1));
            Assert.Equal("Young", EnumEx.GetName<Status>(Status.Young));
        }

        [Fact]
        public void GetDescriptionTest()
        {
            Assert.Equal("少年", EnumEx.GetDescription<Status>(1));
            Assert.Equal("少年", EnumEx.GetDescription<Status>("Young"));
            Assert.Equal("少年", EnumEx.GetDescription<Status>(Status.Young));
        }
    }
}
