using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExtDateTimeTest
    {
        [Fact]
        public void ToDateTimeStringTest()
        {
            var dateTime = DateTime.Parse("2019-01-01 10:10:10");

            Assert.Equal("2019-01-01 10:10:10", dateTime.ToDateTimeString());

            Assert.Equal("2019-01-01 10:10", dateTime.ToDateTimeString(true));

            Assert.Empty(((DateTime?)null).ToDateTimeString());
        }

        [Fact]
        public void ToDateStringTest()
        {
            var dateTime = DateTime.Parse("2019-01-01 10:10:10");

            Assert.Equal("2019-01-01", dateTime.ToDateString());

            Assert.Empty(((DateTime?)null).ToDateTimeString());
        }

        [Fact]
        public void ToTimeStringTest()
        {
            var dateTime = DateTime.Parse("2019-01-01 10:10:10");

            Assert.Equal("10:10:10", dateTime.ToTimeString());

            Assert.Empty(((DateTime?)null).ToTimeString());
        }

        [Fact]
        public void ToMillisecondStringTest()
        {
            var dateTime = DateTime.Parse("2019-01-01 10:10:10.999");

            Assert.Equal("2019-01-01 10:10:10.999", dateTime.ToMillisecondString());

            Assert.Empty(((DateTime?)null).ToDateTimeString());
        }

        [Fact]
        public void ToChineseDateTimeStringTest()
        {
            var dateTime = DateTime.Parse("2019-01-01 10:10:10.999");

            Assert.Equal("2019年1月1日 10时10分10秒", dateTime.ToChineseDateTimeString());

            Assert.Equal("2019年1月1日 10时10分", dateTime.ToChineseDateTimeString(true));

            Assert.Empty(((DateTime?)null).ToChineseDateTimeString());
        }

        [Fact]
        public void ToChineseDateStringTest()
        {
            var dateTime = DateTime.Parse("2019-01-01 10:10:10.999");

            Assert.Equal("2019年1月1日", dateTime.ToChineseDateString());

            Assert.Empty(((DateTime?)null).ToChineseDateString());
        }

        [Fact]
        public void DescriptionTest()
        {
            var timeSpan = TimeSpan.Parse("10:10:10.999");

            Assert.Equal("10小时10分10秒999毫秒", timeSpan.Description());
        }

        [Fact]
        public void GetUnixTimestampTest()
        {

        }

        [Fact]
        public void GetTimeFromUnixTimestamp()
        {

        }
    }
}
