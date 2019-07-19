using System.Reflection;

namespace Codeping.Agile.Core
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取序号, 使用 IndexAttribute 设置序号
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static int GetIndex(this MemberInfo member)
        {
            if (member != null &&
                member.GetCustomAttribute<IndexAttribute>() is IndexAttribute indexAttribute)
            {
                return indexAttribute.Index;
            }

            return -1;
        }
    }
}
