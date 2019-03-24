using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Codeping.Utils;
using System.IO;

namespace Utils.Tests.Ex
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

            Assert.Equal("hello", File.ReadAllText(fullPath));

            FileEx.WriteHideFile(fullPath, "edit");

            Assert.Equal(FileAttributes.Hidden, File.GetAttributes(fullPath));

            Assert.Equal("edit", File.ReadAllText(fullPath));

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }

            FileEx.WriteHideFile(fullPath, "你好", Encoding.GetEncoding("gbk"));

            Assert.NotEqual("你好", File.ReadAllText(fullPath));

            Assert.Equal("你好", File.ReadAllText(fullPath, Encoding.GetEncoding("gbk")));

            FileEx.WriteHideFile(fullPath, "修改", Encoding.GetEncoding("gbk"));

            Assert.NotEqual("修改", File.ReadAllText(fullPath));

            Assert.Equal("修改", File.ReadAllText(fullPath, Encoding.GetEncoding("gbk")));
        }

        [Fact]
        public void GetRelativePathTest()
        {
            string rootPath = @"c:\a\";
            string fullPath = @"c:\a\b";
            Assert.Equal("b", FileEx.GetRelativePath(rootPath, fullPath));

            rootPath = @"c:\a\";
            fullPath = @"c:\a.txt";
            Assert.Equal(@"..\a.txt", FileEx.GetRelativePath(rootPath, fullPath));

            rootPath = @"c:\a\";
            fullPath = @"d:\a.txt";
            Assert.Equal(fullPath, FileEx.GetRelativePath(rootPath, fullPath));

            rootPath = fullPath = @"c:\a.txt";
            Assert.Equal(fullPath, FileEx.GetRelativePath(rootPath, fullPath));

            Assert.Equal("", FileEx.GetRelativePath(rootPath, ""));
        }

        [Fact]
        public void GetAbsolutePathTest()
        {
            string rootPath = @"c:\a\";
            string relativePath = @"b.txt";
            Assert.Equal(@"c:\a\b.txt", FileEx.GetAbsolutePath(rootPath, relativePath));

            rootPath = @"c:\a\b";
            relativePath = @"..\a.txt";
            Assert.Equal(@"c:\a\a.txt", FileEx.GetAbsolutePath(rootPath, relativePath));

            rootPath = @"c:\a\";
            relativePath = @"d:\a.txt";
            Assert.Equal(relativePath, FileEx.GetAbsolutePath(rootPath, relativePath));

            Assert.Equal(@"c:\a\", FileEx.GetAbsolutePath(rootPath, ""));

            Assert.Equal("", FileEx.GetAbsolutePath("", ""));
        }
    }
}
