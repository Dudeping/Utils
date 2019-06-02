using System;
using System.Collections.Generic;
using System.Text;
using Codeping.Utils.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Codeping.Utils.Logging
{
    internal class LoggingConfigureOptions : ManifestEmbeddedFileProviderConfigureOptions
    {
        public LoggingConfigureOptions(IHostingEnvironment host) : base(host)
        {
        }

        protected override string Root => "wwwroot";
    }
}
