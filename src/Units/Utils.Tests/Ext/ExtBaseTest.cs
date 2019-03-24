using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExtBaseTest
    {
        [Fact]
        public void SafeValueTest()
        {
            int? i = null;
            Assert.Equal(0, i.SafeValue());
            i = 1;
            Assert.Equal(1, i.SafeValue());

            DateTime? time = null;
            Assert.Equal(default, time.SafeValue());

            time = DateTime.Parse("2019-01-01");
            Assert.Equal(DateTime.Parse("2019-01-01"), time.SafeValue());
        }

        [Fact]
        public void SafeStringTest()
        {
            Assert.Equal(string.Empty, ((string)null).SafeString());

            Assert.Equal(string.Empty, "".SafeString());

            Assert.Equal("test", " test".SafeString());

            Assert.Equal("str", "  str  ".SafeString());
        }

        [Fact]
        public void CastTest()
        {
            Assert.Equal("9", 9.Cast<string>());

            Assert.Equal(9.0, 9.Cast<double>());

            Assert.True("true".Cast<bool>());

            Assert.False("".Cast<bool>());

            Assert.Equal(default, "".Cast<DateTime>());

            Assert.Null("".Cast<int?>());

            Assert.Equal(default, "true".Cast<DateTime>());

            Assert.Equal(0, 0.Cast<object>());

            Assert.Equal(0, ((int?)null).Cast<int>());

            var child = new Child() { Id = 1 };
            var parent = child.Cast<IParent>();

            Assert.NotNull(parent);
            Assert.Equal(child, parent);

            var guid = Guid.Parse("{DB5CA5B9-E3EF-498B-BF50-6AA65C399263}");
            Assert.Equal(guid, "{DB5CA5B9-E3EF-498B-BF50-6AA65C399263}".Cast<Guid>());

            Assert.Equal(Status.Old, "old".Cast<Status>());
            Assert.Equal((Status)0, "0".Cast<Status>());
            Assert.Equal((Status)100, "100".Cast<Status>());
        }
    }
}
