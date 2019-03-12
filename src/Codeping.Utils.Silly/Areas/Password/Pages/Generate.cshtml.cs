using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Codeping.Utils.Silly.Password.Pages
{
    public class GenerateModel : PageModel
    {
        [DisplayName("密文")]
        public string Password { get; set; }

        [BindProperty]
        [DisplayName("明文")]
        public string Text { get; set; }

        public IActionResult OnPost()
        {
            this.Password = this.Text.Md5By32();

            return Page();
        }
    }
}