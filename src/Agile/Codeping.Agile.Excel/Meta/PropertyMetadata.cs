using System.Reflection;
using Codeping.Agile.Core;
using Codeping.Utils;

namespace Codeping.Agile.Excel
{
    public class PropertyMetadata
    {
        public PropertyMetadata(int index, PropertyInfo info)
        {
            this.Index = index;
            this.PropertyInfo = info;
            this.ProperyName = info.Name;
            this.PropertyIndex = info.GetIndex();
            this.DisplayName = info.GetDisplayName();
        }

        public int Index { get; set; }
        public int PropertyIndex { get; set; }
        public string DisplayName { get; set; }
        public string ProperyName { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }
}
