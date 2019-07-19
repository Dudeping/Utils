using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Codeping.Utils.Silly.Areas.Recovery
{
    public class EnableModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public EnableModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet(string key)
        {
            if (key == _configuration["Logging:TrackKey"])
            {
                RecoveryMiddleware.IsEnable = true;
            }
        }
    }
}