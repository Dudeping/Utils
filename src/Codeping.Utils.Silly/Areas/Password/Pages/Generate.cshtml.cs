using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Codeping.Utils.Silly.Areas.Password.Pages
{
    public class GenerateModel : PageModel
    {
        [DisplayName("ÃÜÎÄ")]
        public string Password { get; set; }

        [BindProperty]
        [DisplayName("Ã÷ÎÄ")]
        public string Text { get; set; }

        public IActionResult OnPost()
        {
            this.Password = this.Text.Md5By32();

            return Page();
        }
    }
}