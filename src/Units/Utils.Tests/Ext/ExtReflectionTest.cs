using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;

namespace Utils.Tests
{
    public class ExtReflectionTest
    {
        [Fact]
        public void GetFullNameTest()
        {
            var child = typeof(Child).GetMethod("Run");
            var name = typeof(Child).GetProperty("Name");
            var ochild = new Child { Name = "Test" };

            Assert.Equal("Utils.Tests.Child.Run", child.GetFullName());
            Assert.Equal("Test", name.GetPropertyValue(ochild));

            Assert.NotNull(typeof(Child).CreateInstance<Child>());
        }

        [Fact]
        public void GetPropertyValueTest()
        {
            var name = typeof(Child).GetProperty("Name");
            var ochild = new Child { Name = "Test" };

            Assert.Equal("Test", name.GetPropertyValue(ochild));
        }

        [Fact]
        public void CreateInstanceTest()
        {
            Assert.NotNull(typeof(Child).CreateInstance<Child>());
        }

        [Fact]
        public void IsDependencyTest()
        {
            var assembly = TypeEx.GetAssembly("Utils.Tests");
            var dependency = TypeEx.GetAssembly("xunit.core");

            Assert.True(assembly.IsDependency("xunit.core"));
            Assert.True(assembly.IsDependency(dependency));

            Assert.False(assembly.IsDependency("Codeping"));
            Assert.False(assembly.IsDependency("Utils.Tests"));
        }

        [Fact]
        public void IsNullable()
        {
            Assert.True(typeof(int?).IsNullable());
            Assert.False(typeof(int).IsNullable());

            Assert.False(typeof(DateTime).IsNullable());
            Assert.False(typeof(string).IsNullable());
        }

        [Fact]
        public void IsBoolTest()
        {
            Assert.True(typeof(bool).IsBool());
            Assert.False(typeof(Child).GetMethod("Run").IsBool());

            Assert.False(((Type)null).IsBool());
        }
    }
}
