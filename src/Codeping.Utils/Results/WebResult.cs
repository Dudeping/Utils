﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Codeping.Utils
{
    public static class WebResult
    {
        public static ContentResult JContent(this PageModel page, string text, string url)
        {
            text = text.Replace('\'', '\0').Replace('\r', '\0').Replace('\n', '\0');

            return page.Content($"<script>alert('{text}'); location.href='{url}';</script>", "text/html", Encoding.UTF8);
        }

        public static ContentResult JContent(this ControllerBase controller, string text, string url)
        {
            text = text.Replace('\'', '\0').Replace('\r', '\0').Replace('\n', '\0');

            return controller.Content($"<script>alert('{text}'); location.href='{url}';</script>", "text/html", Encoding.UTF8);
        }

        public static ContentResult JContent(this ControllerBase controller, OperatResult result, string successUrl, string failUrl)
        {
            return controller.JContent(result.IsError ? result.ErrorMessage : "操作成功!", result.IsError ? failUrl : successUrl);
        }
    }
}
