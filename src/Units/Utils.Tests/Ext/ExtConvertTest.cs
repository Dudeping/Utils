using System;
using Xunit;
using Codeping.Utils;
using System.Text;

namespace Utils.Tests
{
    public class ExtConvertTest
    {
        [Fact]
        public void ToIntTest()
        {
            Assert.Equal(1, 1.2.ToInt());

            Assert.Equal(2, "2".ToInt());

            Assert.Equal(3, " 3 ".ToInt());

            Assert.Equal(default, "error".ToInt());

            Assert.Null("error".ToIntOrNull());

            Assert.Null("".ToIntOrNull());
        }

        [Fact]
        public void ToLongTest()
        {
            Assert.Equal(1, 1.2.ToLong());

            Assert.Equal(2, "2".ToLong());

            Assert.Equal(3, " 3 ".ToLong());

            Assert.Equal(default, "error".ToLong());

            Assert.Null("".ToLongOrNull());

            Assert.Null("error".ToLongOrNull());
        }

        [Fact]
        public void ToFloatTest()
        {
            Assert.Equal((float)1.2, "1.2".ToFloat());

            Assert.Equal((float)1.22, "1.222".ToFloat(2));

            Assert.Equal((float)1.23, "1.226".ToFloat(2));

            Assert.Equal(default, "error".ToFloat());

            Assert.Null("".ToFloatOrNull());

            Assert.Null("error".ToFloatOrNull());
        }

        [Fact]
        public void ToDoubleTest()
        {
            Assert.Equal(1.2, "1.2".ToDouble());

            Assert.Equal(1.22, "1.222".ToDouble(2));

            Assert.Equal(1.23, "1.226".ToDouble(2));

            Assert.Equal(default, "error".ToDouble());

            Assert.Null("".ToDoubleOrNull());

            Assert.Null("error".ToDoubleOrNull());
        }

        [Fact]
        public void ToDecimalTest()
        {
            Assert.Equal((decimal)1.2, "1.2".ToDecimal());

            Assert.Equal((decimal)1.22, "1.222".ToDecimal(2));

            Assert.Equal((decimal)1.23, "1.226".ToDecimal(2));

            Assert.Equal(default, "error".ToDecimal());

            Assert.Null("".ToDecimalOrNull());

            Assert.Null("error".ToDecimalOrNull());
        }

        [Fact]
        public void ToBoolTest()
        {
            Assert.True("true".ToBool());

            Assert.False("false".ToBool());

            Assert.False("  false".ToBool());

            Assert.False("0".ToBool());

            Assert.False("否".ToBool());

            Assert.False("不".ToBool());

            Assert.False("no".ToBool());

            Assert.False("fail".ToBool());

            Assert.True("1".ToBool());

            Assert.True("是".ToBool());

            Assert.True("yes".ToBool());

            Assert.True("ok".ToBool());

            Assert.False("".ToBool());

            Assert.False("error".ToBool());

            Assert.Null("".ToBoolOrNull());

            Assert.Null("error".ToBoolOrNull());
        }

        [Fact]
        public void ToDateTest()
        {
            Assert.Equal(DateTime.Parse("2019-01-01"), "2019-01-01".ToDate());

            Assert.Equal(DateTime.Parse("2019/01/01"), "2019/01/01".ToDate());

            Assert.Equal(DateTime.Parse("2019/01/01 10:10"), "2019/01/01 10:10".ToDate());

            Assert.Equal(default, "".ToDate());

            Assert.Equal(default, ((int?)null).ToDate());

            Assert.Null("".ToDateOrNull());

            Assert.Null("error".ToDateOrNull());
        }

        [Fact]
        public void ToGuidTest()
        {
            var text = "{71B3CF75-569D-489F-9C5B-E237781FE494}";
            Guid? guid = Guid.Parse(text);

            Assert.Equal(guid, text.ToGuid());

            Assert.Equal(default, "error".ToGuidOrNull());
        }

        [Fact]
        public void ToBytesTest()
        {
            Assert.Equal(5, "bytes".ToBytes().Length);

            Assert.Equal(6, "好人".ToBytes().Length);

            Assert.Equal(4, "好人".ToBytes(Encoding.Unicode).Length);
        }

        [Fact]
        public void IsEmptyTest()
        {
            Assert.True("".IsEmpty());

            Assert.True(" ".IsEmpty());

            Assert.True(((string)null).IsEmpty());

            Assert.False("not".IsEmpty());

            Guid? guid = Guid.Empty;

            Assert.True(guid.IsEmpty());

            Assert.False(Guid.Parse("{CB84590F-34F9-4235-B5EF-CEAC06C792C9}").IsEmpty());
        }

        [Fact]
        public void ToListTest()
        {
            Assert.Empty("".ToList<string>(","));

            var ints = "1,3".ToList<int>(",");
            Assert.Equal(2, ints.Count);
            Assert.Equal(1, ints[0]);
            Assert.Equal(3, ints[1]);

            var strs = "1|2|3".ToList<string>("|");
            Assert.Equal(3, strs.Count);
            Assert.Equal("1", strs[0]);
            Assert.Equal("2", strs[1]);
            Assert.Equal("3", strs[2]);
        }
    }
}
