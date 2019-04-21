using System;

namespace Codeping.Agile.Core
{
    /// <summary>
    /// 从零开始
    /// </summary>
    public class IndexAttribute : Attribute
    {
        public IndexAttribute(int index)
        {
            this.Index = index;
        }

        public int Index { get; set; }
    }
}
