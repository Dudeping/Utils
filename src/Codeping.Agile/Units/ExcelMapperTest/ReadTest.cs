using System.Linq;
using Codeping.Utils.ExcelMapper;
using Xunit;

namespace ExcelMapperTest
{
    public class ReadTest
    {
        [Fact]
        public void ReadSingleModel()
        {
            var em = new ExcelReader("Data\\users.xlsx");

            var users = em.Read<User>(new Settings());

            Assert.NotNull(users);
            Assert.Equal(5, users.Count());
        }

        [Fact]
        public void ReadTupleModel()
        {
            var em = new ExcelReader("Data\\users_role.xlsx");

            var users = em.Read<User, Role>(new Settings());

            Assert.NotNull(users);
            Assert.Equal(5, users.Count());
        }
    }
}
