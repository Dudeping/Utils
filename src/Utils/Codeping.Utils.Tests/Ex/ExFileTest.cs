using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;
using System.IO;

namespace Utils.Tests
{
    public class ExFileTest
    {
        [Fact]
        public void WriteHideFileTest()
        {
            var fullPath = Path.GetFullPath("test\\test.txt");

            var directory = Path.GetDirectoryName(fullPath);

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            FileEx.WriteHideFile(fullPath, "hello");

            Assert.Equal(FileAttributes.Hidden, File.GetAttributes(fullPath));

            File.SetAttributes(directory, FileAttributes.Hidden);

            Assert.Equal("hello", File.ReadAllText(fullPath));

            FileEx.WriteHideFile(fullPath, "edit");

            Assert.Equal(FileAttributes.Hidden, File.GetAttributes(fullPath));

            Assert.Equal("edit", File.ReadAllText(fullPath));

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            FileEx.WriteHideFile(fullPath, "你好", Encoding.GetEncoding("gbk"));

            File.SetAttributes(directory, FileAttributes.Hidden);

            Assert.NotEqual("你好", File.ReadAllText(fullPath));

            Assert.Equal("你好", File.ReadAllText(fullPath, Encoding.GetEncoding("gbk")));

            FileEx.WriteHideFile(fullPath, "修改", Encoding.GetEncoding("gbk"));

            Assert.NotEqual("修改", File.ReadAllText(fullPath));

            Assert.Equal("修改", File.ReadAllText(fullPath, Encoding.GetEncoding("gbk")));
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(@"c:\a\", "", "")]
        [InlineData(@"c:\a\", @"c:\a\b", "b")]
        [InlineData(@"c:\a\", @"c:\a.txt", @"..\a.txt")]
        [InlineData(@"c:\a\", @"d:\a.txt", @"d:\a.txt")]
        [InlineData(@"c:\a.txt", @"c:\a.txt", @"c:\a.txt")]
        public void GetRelativePathTest(
            string rootPath, string fullPath, string relativePath)
        {
            Assert.Equal(relativePath, FileEx.GetRelativePath(rootPath, fullPath));
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(@"c:\a\", @"c:\a\", @"c:\a\")]
        [InlineData(@"c:\a.txt", "", @"c:\a.txt")]
        [InlineData(@"c:\a\", @"d:\a.txt", @"d:\a.txt")]
        [InlineData(@"c:\a\", @"b.txt", @"c:\a\b.txt")]
        [InlineData(@"c:\a\b", @"..\a.txt", @"c:\a\a.txt")]
        public void GetAbsolutePathTest(
            string rootPath, string relativePath, string fullPath)
        {
            Assert.Equal(fullPath, FileEx.GetAbsolutePath(rootPath, relativePath));
        }

        [Fact]
        public void GetSize()
        {
            Assert.Equal(1000, FileEx.GetSize(1000, FileSizeUnit.B));
            Assert.Equal(1024000, FileEx.GetSize(1000, FileSizeUnit.K));

            Assert.Equal(1024 * 1024, FileEx.GetSize(1, FileSizeUnit.M));
            Assert.Equal(1024 * 1024 * 1024, FileEx.GetSize(1, FileSizeUnit.G));
        }

        [Fact]
        public void GetFileSize()
        {
            var fullPath = Path.GetFullPath("test\\test.txt");

            var directory = Path.GetDirectoryName(fullPath);

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            FileEx.WriteHideFile(fullPath, "hello");

            File.SetAttributes(directory, FileAttributes.Hidden);

            Assert.Equal(5, FileEx.GetSize(fullPath).GetSize());
        }
    }
}
