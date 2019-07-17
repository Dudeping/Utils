using System;
using System.Globalization;
using Xunit;

namespace Codeping.Constants.Tests
{
    public class DateFormatTest
    {
        private readonly DateTime _dateTime = new DateTime(2019, 1, 2, 3, 4, 5, 666, DateTimeKind.Local);

        [Theory]
        [InlineData(F_Date.Fd, C_Lang.zh_cn, "2019/1/2")]
        [InlineData(F_Date.Fd, C_Lang.en_us, "1/2/2019")]

        [InlineData(F_Date.FD, C_Lang.zh_cn, "2019年1月2日")]
        [InlineData(F_Date.FD, C_Lang.en_us, "Wednesday, January 2, 2019")]

        [InlineData(F_Date.Ff, C_Lang.zh_cn, "2019年1月2日 3:04")]
        [InlineData(F_Date.Ff, C_Lang.en_us, "Wednesday, January 2, 2019 3:04 AM")]

        [InlineData(F_Date.FF, C_Lang.zh_cn, "2019年1月2日 3:04:05")]
        [InlineData(F_Date.FF, C_Lang.en_us, "Wednesday, January 2, 2019 3:04:05 AM")]

        [InlineData(F_Date.Fg, C_Lang.zh_cn, "2019/1/2 3:04")]
        [InlineData(F_Date.Fg, C_Lang.en_us, "1/2/2019 3:04 AM")]

        [InlineData(F_Date.FG, C_Lang.zh_cn, "2019/1/2 3:04:05")]
        [InlineData(F_Date.FG, C_Lang.en_us, "1/2/2019 3:04:05 AM")]

        [InlineData(F_Date.Fm, C_Lang.zh_cn, "1月2日")]
        [InlineData(F_Date.Fm, C_Lang.en_us, "January 2")]

        [InlineData(F_Date.FM, C_Lang.zh_cn, "1月2日")]
        [InlineData(F_Date.FM, C_Lang.en_us, "January 2")]

        [InlineData(F_Date.Fr, C_Lang.zh_cn, "Wed, 02 Jan 2019 03:04:05 GMT")]
        [InlineData(F_Date.Fr, C_Lang.en_us, "Wed, 02 Jan 2019 03:04:05 GMT")]

        [InlineData(F_Date.FR, C_Lang.zh_cn, "Wed, 02 Jan 2019 03:04:05 GMT")]
        [InlineData(F_Date.FR, C_Lang.en_us, "Wed, 02 Jan 2019 03:04:05 GMT")]

        [InlineData(F_Date.Fs, C_Lang.zh_cn, "2019-01-02T03:04:05")]
        [InlineData(F_Date.Fs, C_Lang.en_us, "2019-01-02T03:04:05")]

        [InlineData(F_Date.Ft, C_Lang.zh_cn, "3:04")]
        [InlineData(F_Date.Ft, C_Lang.en_us, "3:04 AM")]

        [InlineData(F_Date.FT, C_Lang.zh_cn, "3:04:05")]
        [InlineData(F_Date.FT, C_Lang.en_us, "3:04:05 AM")]

        [InlineData(F_Date.Fu, C_Lang.zh_cn, "2019-01-02 03:04:05Z")]
        [InlineData(F_Date.Fu, C_Lang.en_us, "2019-01-02 03:04:05Z")]

        [InlineData(F_Date.FU, C_Lang.zh_cn, "2019年1月1日 19:04:05")]
        [InlineData(F_Date.FU, C_Lang.en_us, "Tuesday, January 1, 2019 7:04:05 PM")]

        [InlineData(F_Date.Fy, C_Lang.zh_cn, "2019年1月")]
        [InlineData(F_Date.Fy, C_Lang.en_us, "January 2019")]

        [InlineData(F_Date.FY, C_Lang.zh_cn, "2019年1月")]
        [InlineData(F_Date.FY, C_Lang.en_us, "January 2019")]
        public void FormatFdTest(string format, string name, string expected)
        {
            var culture = CultureInfo.CreateSpecificCulture(name);
            var v = _dateTime.ToString(format, culture);

            Assert.NotNull(v);
            Assert.NotEmpty(v);
            Assert.Equal(expected, v);
        }
    }
}
