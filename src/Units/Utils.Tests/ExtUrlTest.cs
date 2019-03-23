using Codeping.Utils;
using Xunit;

namespace Utils.Tests
{
    public class ExtUrlTest
    {
        [Fact]
        public void CombineTest()
        {
            Assert.Equal("http://www.a.cn/a", "http://www.a.cn/".Combine("a"));

            Assert.Equal("http://www.a.cn/a/b", "http://www.a.cn/".Combine("a/b"));

            Assert.Equal("http://www.a.cn?a=1", "http://www.a.cn".CombineParam("a=1"));

            Assert.Equal("http://www.a.cn?a=1", "http://www.a.cn".CombineParam("a=1"));

            Assert.Equal("http://www.a.cn?a=1&b=2", "http://www.a.cn".CombineParam("a=1&b=2"));

            Assert.Equal("http://www.a.cn?a=1", "http://www.a.cn?".CombineParam("a=1"));

            Assert.Equal("http://www.a.cn?a=1&b=2", "http://www.a.cn?a=1&".CombineParam("b=2"));
        }
    }
}
