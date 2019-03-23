using Newtonsoft.Json;

namespace Codeping.Utils
{
    /// <summary>
    /// 系统扩展 - Json 操作
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// 将 Json 字符串转换为对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        public static T ToObject<T>(this string json)
        {
            return json.IsEmpty() ? default : JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将对象转换为 Json 字符串
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="isConvertToSingleQuotes">是否将双引号转成单引号</param>
        public static string ToJson(this object target, bool isConvertToSingleQuotes = false)
        {
            if (target == null)
            {
                return "{}";
            }

            string result = JsonConvert.SerializeObject(target);

            if (isConvertToSingleQuotes)
            {
                result = result.Replace("\"", "'");
            }

            return result;
        }
    }
}
