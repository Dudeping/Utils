using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;

namespace Codeping.Utils.Logging
{
    public class LogEntry
    {
        public int Id { get; set; }

        /// <summary>
        /// 日志编号
        /// </summary>
        [DisplayName("编号")]
        public string Num { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("内容")]
        public string Content { get; set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        [DisplayName("堆栈信息")]
        public string StackTrace { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 日志级别
        /// </summary>
        [DisplayName("日志级别")]
        public LogLevel Level { get; set; }

        /// <summary>
        /// 触发者
        /// </summary>
        [DisplayName("触发者")]
        public string Sender { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        [NotMapped]
        [DisplayName("异常")]
        internal Exception Exception { get; set; }
    }
}
