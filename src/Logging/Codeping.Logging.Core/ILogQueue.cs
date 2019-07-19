namespace Codeping.Utils.Logging
{
    internal interface ILogQueue
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="entry"></param>
        void Write(LogEntry entry);

        /// <summary>
        /// 将日志加入队列
        /// </summary>
        /// <param name="entry"></param>
        void Enqueue(LogEntry entry);
    }
}