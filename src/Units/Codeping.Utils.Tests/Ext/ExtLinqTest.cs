using Codeping.Utils;
using System.Collections;
using System.Linq;
using Xunit;

namespace Utils.Tests
{
    public class ExtLinqTest
    {
        [Fact]
        public void RandomSortTest()
        {
            int[] ints = new[] { 1, 2, 3 };

            Assert.Equal(ints.Sum(), ints.RandomSort().Sum());

            ints = null;
            Assert.Empty(ints.RandomSort());
        }

        [Fact]
        public void JoinTest()
        {
            Assert.Equal("a,b", new[] { "a", "b" }.Join());

            Assert.Equal("'a','b'", new[] { "a", "b" }.Join(quotes: "'"));

            Assert.Equal("a|b", new[] { "a", "b" }.Join("|"));

            Assert.Equal("`a`@`b`", new[] { "a", "b" }.Join("@", "`"));

            Assert.Empty(((int[])null).Join());
        }

        [Fact]
        public void ToListTest()
        {
            int[] ints = new[] { 1, 2, 3 };

            Assert.Equal(3, ints.ToList(x => x >= 3).Sum());

            Assert.Equal(3, ((IEnumerable)ints).ToList<int>(x => x >= 3).Sum());

            Assert.Equal(3, ints.ToList(x => x - 1).Sum());

            Assert.Equal(3, ((IEnumerable)ints).ToList<int, int>(x => x - 1).Sum());
        }
    }
}
