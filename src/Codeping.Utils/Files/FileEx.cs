using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Codeping.Utils
{
    /// <summary>
    /// 文件和流操作
    /// </summary>
    public static class FileEx
    {
        /// <summary>
        /// 创建一个新文件，将指定的字符串写入该文件，然后关闭该文件。 如果目标文件已存在，则会被覆盖。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        public static void WriteHideFile(string path, string contents)
        {
            var directory = Path.GetDirectoryName(path);

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
        /// 创建一个新文件，将指定的字符串写入该文件，然后关闭该文件。 如果目标文件已存在，则会被覆盖。
        /// </summary>
        /// <param name="path">要写入的文件。</param>
        /// <param name="contents">要写入文件的字符串。</param>
        /// <param name="encoding">要应用于字符串的编码。</param>
        public static void WriteHideFile(string path, string contents, Encoding encoding)
        {
            var directory = Path.GetDirectoryName(path);

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
        /// 流转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">读取完成是否释放流, 默认为true</param>
        public static string ToString(Stream stream, Encoding encoding = null, int bufferSize = 1024 * 2, bool isCloseStream = true)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (stream.CanRead == false)
            {
                return string.Empty;
            }

            using (StreamReader reader = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                string result = reader.ReadToEnd();
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                return result;
            }
        }

        /// <summary>
        /// 流转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">读取完成是否释放流, 默认为true</param>
        public static async Task<string> ToStringAsync(Stream stream, Encoding encoding = null, int bufferSize = 1024 * 2, bool isCloseStream = true)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (stream.CanRead == false)
            {
                return string.Empty;
            }

            using (StreamReader reader = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                string result = await reader.ReadToEndAsync();
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                return result;
            }
        }

        /// <summary>
        /// 复制流并转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        public static async Task<string> CopyToStringAsync(Stream stream, Encoding encoding = null)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            if (stream.CanRead == false)
            {
                return string.Empty;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream, encoding))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                stream.CopyTo(memoryStream);
                if (memoryStream.CanSeek)
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }

                string result = await reader.ReadToEndAsync();
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                return result;
            }
        }
    }
}
