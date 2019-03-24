using System;
using System.IO;
using System.Text;

namespace Codeping.Utils
{
    /// <summary>
    /// 文件和流操作
    /// </summary>
    public static class FileEx
    {
        static FileEx()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 创建一个新隐藏文件，将指定的字符串写入该文件，然后关闭该文件。 如果目标文件已存在，则会被覆盖。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void WriteHideFile(string path, string contents)
        {
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }

            File.WriteAllText(path, contents);

            File.SetAttributes(path, FileAttributes.Hidden);
        }

        /// <summary>
        /// 创建一个新隐藏文件，将指定的字符串写入该文件，然后关闭该文件。 如果目标文件已存在，则会被覆盖。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">要应用于字符串的编码。</param>
        public static void WriteHideFile(string path, string contents, Encoding encoding)
        {
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }

            File.WriteAllText(path, contents, encoding);

            File.SetAttributes(path, FileAttributes.Hidden);
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="rootPath">根目录</param>
        /// <param name="fullPath">全路径</param>
        /// <returns></returns>
        public static string GetRelativePath(string rootPath, string fullPath)
        {
            if (rootPath.Equals(fullPath, StringComparison.OrdinalIgnoreCase))
            {
                return fullPath;
            }

            if (Path.IsPathRooted(rootPath) &&
                Path.IsPathRooted(fullPath) &&
                Path.GetPathRoot(rootPath) != Path.GetPathRoot(fullPath))
            {
                return fullPath;
            }

            string[] absoluteDirectories = rootPath.Split('\\');
            string[] relativeDirectories = fullPath.Split('\\');

            int length = absoluteDirectories.Length < relativeDirectories.Length
                ? absoluteDirectories.Length : relativeDirectories.Length;

            int lastCommonRoot = -1;
            int index;

            for (index = 0; index < length; index++)
            {
                if (absoluteDirectories[index] == relativeDirectories[index])
                {
                    lastCommonRoot = index;
                }
                else
                {
                    break;
                }
            }

            if (lastCommonRoot == -1)
            {
                return "";
            }

            StringBuilder relativePath = new StringBuilder();

            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
            {
                if (absoluteDirectories[index].Length > 0)
                {
                    relativePath.Append("..\\");
                }
            }

            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
            {
                relativePath.Append(relativeDirectories[index] + "\\");
            }

            relativePath.Append(relativeDirectories[^1]);

            return relativePath.ToString();
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="rootPath">根路径</param>
        /// <param name="relativePath">相对路径</param>
        /// <returns></returns>
        public static string GetAbsolutePath(string rootPath, string relativePath)
        {
            string fullPath = Path.IsPathRooted(relativePath)
                ? relativePath : Path.Combine(rootPath, relativePath);

            if (fullPath.IsEmpty())
            {
                return fullPath;
            }

            var info = new FileInfo(fullPath);

            return info.FullName;
        }

        /// <summary>
        ///换算文件大小
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="unit">计量单位</param>
        /// <returns></returns>
        public static long GetSize(long size, FileSizeUnit unit)
        {
            switch (unit)
            {
                case FileSizeUnit.K:
                    return size * 1024;
                case FileSizeUnit.M:
                    return size * 1024 * 1024;
                case FileSizeUnit.G:
                    return size * 1024 * 1024 * 1024;
                default:
                    return size;
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="size">长度</param>
        /// <param name="unit">计量单位</param>
        /// <returns></returns>
        public static FileSize GetSize(string fullPath)
        {
            var info = new FileInfo(fullPath);

            return new FileSize(info.Exists ? info.Length : 0);
        }
    }
}
