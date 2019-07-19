using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Codeping.Utils.Logging.Areas.UtilLog.Pages
{
    public class MessageModel : PageModel
    {
        public string Num { get; set; }

        public void OnGet(string num)
        {
            this.Num = num;
        }
    }
}