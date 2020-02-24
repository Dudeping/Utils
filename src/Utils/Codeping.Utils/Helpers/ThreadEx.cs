using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codeping.Utils
{
    /// <summary>
    /// 线程操作
    /// </summary>
    public static class ThreadEx
    {
        /// <summary>
        /// 执行多个操作, 等待所有操作完成
        /// </summary>
        /// <param name="actions">操作集合</param>
        public static void WaitAll([NotNull]params Action[] actions)
        {
            var tasks = new List<Task>();

            foreach (var action in actions)
            {
                tasks.Add(Task.Factory.StartNew(action, TaskCreationOptions.None));
            }

            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// 并发执行多个操作
        /// </summary>
        /// <param name="actions">操作集合</param>
        public static void Parallel([NotNull]params Action[] actions)
        {
            System.Threading.Tasks.Parallel.Invoke(actions);
        }

        /// <summary>
        /// 重复的并发执行操作
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="count">执行次数</param>
        /// <param name="options">并发执行配置</param>
        public static void Parallel([NotNull]Action action, int count = 1, ParallelOptions options = null)
        {
            ThreadEx.Parallel(x => action(), count, options);
        }

        /// <summary>
        /// 重复的并发执行操作
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="count">执行次数</param>
        /// <param name="options">并发执行配置</param>
        public static void Parallel([NotNull]Action<int> action, int count = 1, ParallelOptions options = null)
        {
            _ = options != null
                ? System.Threading.Tasks.Parallel.For(0, count, options, action)
                : System.Threading.Tasks.Parallel.For(0, count, action);
        }
    }
}
