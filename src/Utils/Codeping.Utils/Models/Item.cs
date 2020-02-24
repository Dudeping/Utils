using System;

namespace Codeping.Utils
{
    /// <summary>
    /// 列表项
    /// </summary>
    public class Item : IComparable<Item>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        public Item(string text, object value, int sortId = 0)
        {
            this.Text = text;
            this.Value = value;
            this.SortId = sortId;
        }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other">其它列表项</param>
        public int CompareTo(Item other)
        {
            return String.Compare(this.Text, other.Text, StringComparison.CurrentCulture);
        }
    }
}
