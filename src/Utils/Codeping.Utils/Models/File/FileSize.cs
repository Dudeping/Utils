namespace Codeping.Utils
{
    /// <summary>
    /// 文件大小
    /// </summary>
    public struct FileSize
    {
        /// <summary>
        /// 初始化文件大小
        /// </summary>
        /// <param name="size">文件大小</param>
        /// <param name="unit">文件大小单位</param>
        public FileSize(long size, FileSizeUnit unit = FileSizeUnit.B)
        {
            this.Size = FileEx.GetSize(size, unit);
        }

        /// <summary>
        /// 文件字节长度
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// 获取文件大小, 单位：字节
        /// </summary>
        public int GetSize()
        {
            return (int)this.Size;
        }

        /// <summary>
        /// 获取文件大小, 单位：K
        /// </summary>
        public double GetSizeByK()
        {
            return (this.Size / 1024.0, 2).ToDouble();
        }

        /// <summary>
        /// 获取文件大小, 单位：M
        /// </summary>
        public double GetSizeByM()
        {
            return (this.Size / 1024.0 / 1024.0, 2).ToDouble();
        }

        /// <summary>
        /// 获取文件大小, 单位：G
        /// </summary>
        public double GetSizeByG()
        {
            return (this.Size / 1024.0 / 1024.0 / 1024.0, 2).ToDouble();
        }

        /// <summary>
        /// 输出描述
        /// </summary>
        public override string ToString()
        {
            if (this.Size >= 1024 * 1024 * 1024)
            {
                return $"{this.GetSizeByG()} {FileSizeUnit.G.Description()}";
            }

            if (this.Size >= 1024 * 1024)
            {
                return $"{this.GetSizeByM()} {FileSizeUnit.M.Description()}";
            }

            if (this.Size >= 1024)
            {
                return $"{this.GetSizeByK()} {FileSizeUnit.K.Description()}";
            }

            return $"{this.Size} {FileSizeUnit.B.Description()}";
        }
    }
}