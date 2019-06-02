using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Codeping.Utils.Mvc
{
    public class UtilPage : PageModel
    {
        public IActionResult Message(string text, string url)
        {
            return Ext.Message(this, text, url);
        }
    }
}
