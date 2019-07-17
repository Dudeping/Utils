using System.Globalization;
using Xunit;

namespace Codeping.Constants.Tests
{
    public class NumberFormatTest
    {
        [Theory]

        [InlineData(F_Num.C, C_Lang.zh_cn, 25, "¥25.00")]
        [InlineData(F_Num.C, C_Lang.en_us, 25, "$25.00")]

        [InlineData(F_Num.D + "4", C_Lang.zh_cn, 25, "0025")]
        [InlineData(F_Num.D + "4", C_Lang.en_us, 25, "0025")]

        [InlineData(F_Num.E, C_Lang.zh_cn, 25, "2.500000E+001")]
        [InlineData(F_Num.E, C_Lang.en_us, 25, "2.500000E+001")]
        [InlineData(F_Num.E + "2", C_Lang.zh_cn, 25, "2.50E+001")]
        [InlineData(F_Num.E + "2", C_Lang.en_us, 25, "2.50E+001")]

        [InlineData(F_Num.e, C_Lang.zh_cn, 25, "2.500000e+001")]
        [InlineData(F_Num.e, C_Lang.en_us, 25, "2.500000e+001")]
        [InlineData(F_Num.e + "2", C_Lang.zh_cn, 25, "2.50e+001")]
        [InlineData(F_Num.e + "2", C_Lang.en_us, 25, "2.50e+001")]

        [InlineData(F_Num.F, C_Lang.zh_cn, 25, "25.00")]
        [InlineData(F_Num.F, C_Lang.en_us, 25, "25.00")]
        [InlineData(F_Num.F + "1", C_Lang.zh_cn, 25, "25.0")]
        [InlineData(F_Num.F + "1", C_Lang.en_us, 25, "25.0")]

        [InlineData(F_Num.N, C_Lang.zh_cn, 2500, "2,500.00")]
        [InlineData(F_Num.N, C_Lang.en_us, 2500, "2,500.00")]

        [InlineData(F_Num.P, C_Lang.zh_cn, 25, "2,500.00%")]
        [InlineData(F_Num.P, C_Lang.en_us, 25, "2,500.00%")]
        [InlineData(F_Num.P + "1", C_Lang.zh_cn, 25, "2,500.0%")]
        [InlineData(F_Num.P + "1", C_Lang.en_us, 25, "2,500.0%")]

        [InlineData(F_Num.X, C_Lang.zh_cn, 16, "10")]
        [InlineData(F_Num.X, C_Lang.en_us, 16, "10")]
        [InlineData(F_Num.X, C_Lang.zh_cn, 10, "A")]
        [InlineData(F_Num.X, C_Lang.en_us, 10, "A")]

        public void IntNumberTest(string format, string name, int source, string expected)
        {
            var culture = CultureInfo.CreateSpecificCulture(name);

            Assert.Equal(expected, source.ToString(format, culture));
        }

        [Theory]

        [InlineData(F_Num.C, C_Lang.zh_cn, 2.5, "¥2.50")]
        [InlineData(F_Num.C, C_Lang.en_us, 2.5, "$2.50")]

        [InlineData(F_Num.E, C_Lang.zh_cn, 0.25, "2.500000E-001")]
        [InlineData(F_Num.E, C_Lang.en_us, 0.25, "2.500000E-001")]
        [InlineData(F_Num.E + "2", C_Lang.zh_cn, 0.25, "2.50E-001")]
        [InlineData(F_Num.E + "2", C_Lang.en_us, 0.25, "2.50E-001")]

        [InlineData(F_Num.e, C_Lang.zh_cn, 0.25, "2.500000e-001")]
        [InlineData(F_Num.e, C_Lang.en_us, 0.25, "2.500000e-001")]
        [InlineData(F_Num.e + "2", C_Lang.zh_cn, 0.25, "2.50e-001")]
        [InlineData(F_Num.e + "2", C_Lang.en_us, 0.25, "2.50e-001")]

        [InlineData(F_Num.F, C_Lang.zh_cn, 2.5, "2.50")]
        [InlineData(F_Num.F, C_Lang.en_us, 2.5, "2.50")]
        [InlineData(F_Num.F + "3", C_Lang.zh_cn, 2.5, "2.500")]
        [InlineData(F_Num.F + "3", C_Lang.en_us, 2.5, "2.500")]

        [InlineData(F_Num.N, C_Lang.zh_cn, 2500.5, "2,500.50")]
        [InlineData(F_Num.N, C_Lang.en_us, 2500.5, "2,500.50")]

        [InlineData(F_Num.P, C_Lang.zh_cn, 0.25, "25.00%")]
        [InlineData(F_Num.P, C_Lang.en_us, 0.25, "25.00%")]
        [InlineData(F_Num.P + "1", C_Lang.zh_cn, 0.25, "25.0%")]
        [InlineData(F_Num.P + "1", C_Lang.en_us, 0.25, "25.0%")]

        // Custom
        [InlineData("00.00%", C_Lang.zh_cn, 0.25, "25.00%")]
        [InlineData("00.00%", C_Lang.en_us, 0.25, "25.00%")]
        [InlineData("00.00%", C_Lang.zh_cn, 0.255, "25.50%")]
        [InlineData("00.00%", C_Lang.en_us, 0.255, "25.50%")]
        [InlineData("00.00%", C_Lang.zh_cn, 0.0255, "02.55%")]
        [InlineData("00.00%", C_Lang.en_us, 0.0255, "02.55%")]
        [InlineData("##.##%", C_Lang.zh_cn, 0.0255, "2.55%")]
        [InlineData("##.##%", C_Lang.en_us, 0.0255, "2.55%")]
        public void FloatNumberTest(string format, string name, double source, string expected)
        {
            var culture = CultureInfo.CreateSpecificCulture(name);

            Assert.Equal(expected, source.ToString(format, culture));
        }
    }
}
