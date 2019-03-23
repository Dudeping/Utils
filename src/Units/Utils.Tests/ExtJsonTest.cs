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
            var model = new JsonTest { Id = 1, Name = "Test" };

            Assert.Equal("{\"Id\":1,\"Name\":\"Test\"}", model.ToJson());
        }

        [Fact]
        public void ToObjectTest()
        {
            var model = "{\"Id\":2,\"Name\":\"Test\"}".ToObject<JsonTest>();

            Assert.Equal(2, model.Id);

            Assert.Equal("Test", model.Name);
        }
    }

    class JsonTest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
