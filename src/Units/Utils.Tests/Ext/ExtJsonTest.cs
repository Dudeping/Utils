using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExtJsonTest
    {
        [Fact]
        public void ToJsonTest()
        {
            var model = new Parent { Id = 1, Name = "Test" };

            Assert.Contains("'Id':1,'Name':'Test'", model.ToJson(true));
            Assert.Contains("\"Id\":1,\"Name\":\"Test\"", model.ToJson());

            model = null;
            Assert.Equal("{}", model.ToJson());
        }

        [Fact]
        public void ToObjectTest()
        {
            var model = "{\"Id\":2,\"Name\":\"Test\"}".ToObject<Parent>();

            Assert.Equal(2, model.Id);

            Assert.Equal("Test", model.Name);
        }
    }
}
