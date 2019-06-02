using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace Codeping.Utils.Mvc
{
    public static partial class Ext
    {
        public static ContentResult Message(this PageModel _, string text, string url)
        {
            text = text.Replace('\'', '\0').Replace('\r', '\0').Replace('\n', '\0');

            var mediaType = MediaTypeHeaderValue.Parse("text/html");

            mediaType.Encoding = Encoding.UTF8;

            return new ContentResult
            {
                ContentType = mediaType.ToString(),
                Content = $"<script>alert('{text}'); location.href='{url}';</script>",
            };
        }

        public static ContentResult Message(this PageModel page, Result result, string successUrl, string failUrl)
        {
            return page.Message(result.Succeeded ? result.ErrorMessage : "操作成功!", result.Succeeded ? failUrl : successUrl);
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
