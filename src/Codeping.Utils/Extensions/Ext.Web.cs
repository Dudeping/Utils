using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeping.Utils
{
    public static partial class Ext
    {
        public static ContentResult Message(this PageModel page, string text, string url)
        {
            text = text.Replace('\'', '\0').Replace('\r', '\0').Replace('\n', '\0');

            return page.Content($"<script>alert('{text}'); location.href='{url}';</script>", "text/html", Encoding.UTF8);
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
