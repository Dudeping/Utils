using Codeping.Agile.Core;

namespace Codeping.Agile.Excel
{
    public class Settings
    {
        /// <summary>
        /// 映射方式
        /// </summary>
        public bool HasHeader { get; set; } = true;

        /// <summary>
        /// 第一行是否为列名
        /// </summary>
        public MapingMethod MapingMethod { get; set; } = MapingMethod.PropertyName;
    }
}
