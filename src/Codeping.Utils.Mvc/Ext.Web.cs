using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Codeping.Utils
{
    public static partial class Ext
    {
        public static ContentResult Message(this IFilterMetadata page, string text, string url)
        {
            text = text.Replace('\'', '\0').Replace('\r', '\0').Replace('\n', '\0');

            return new ContentResult
            {
                Content = $"<script>alert('{text}'); location.href='{url}';</script>",
                ContentType = "text/html",
            };
        }

        public static ContentResult Message(this ControllerBase controller, string text, string url)
        {
            text = text.Replace('\'', '\0').Replace('\r', '\0').Replace('\n', '\0');

            return controller.Content($"<script>alert('{text}'); location.href='{url}';</script>", "text/html", Encoding.UTF8);
        }

        public static ContentResult Message(this ControllerBase controller, Result result, string successUrl, string failUrl)
        {
            return controller.Message(result.Succeeded ? result.ErrorMessage : "操作成功!", result.Succeeded ? failUrl : successUrl);
        }
    }
}
