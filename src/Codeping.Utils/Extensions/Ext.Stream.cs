using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Codeping.Utils
{
    public static partial class Ext
    {

        /// <summary>
        /// 流转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">读取完成是否释放流, 默认为true</param>
        public static string ToText(
            this Stream stream, Encoding encoding = null, int bufferSize = 1024 * 2, bool isCloseStream = true)
        {
            if (stream == null ||
                !stream.CanRead)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamReader sr = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                string result = sr.ReadToEnd();

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
        public static async Task<string> ToTextAsync(
            this Stream stream, Encoding encoding = null, int bufferSize = 1024 * 2, bool isCloseStream = true)
        {
            if (stream == null ||
                !stream.CanRead)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (StreamReader sr = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                string result = await sr.ReadToEndAsync();

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
        public static string CopyToText(
            this Stream stream, Encoding encoding = null, bool isCloseStream = true)
        {
            if (stream == null ||
                !stream.CanRead)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (MemoryStream ms = new MemoryStream())
            using (StreamReader sr = new StreamReader(ms, encoding, !isCloseStream))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                stream.CopyTo(ms);

                if (ms.CanSeek)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                }

                string result = sr.ReadToEnd();

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
        public static async Task<string> CopyToTextAsync(
            this Stream stream, Encoding encoding = null, bool isCloseStream = true)
        {
            if (stream == null ||
                !stream.CanRead)
            {
                return string.Empty;
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (MemoryStream ms = new MemoryStream())
            using (StreamReader sr = new StreamReader(ms, encoding, !isCloseStream))
            {
                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                stream.CopyTo(ms);

                if (ms.CanSeek)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                }

                string result = await sr.ReadToEndAsync();

                if (stream.CanSeek)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                }

                return result;
            }
        }
    }
}
