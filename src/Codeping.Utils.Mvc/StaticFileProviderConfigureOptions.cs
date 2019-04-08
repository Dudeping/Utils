using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;

namespace Codeping.Utils.Mvc
{
    public abstract class ManifestEmbeddedFileConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        protected readonly IHostingEnvironment _host;

        public ManifestEmbeddedFileConfigureOptions(IHostingEnvironment host)
        {
            _host = host;
        }

        protected abstract string Root { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && _host.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider ??= _host.WebRootFileProvider;

            var fileProvider = new ManifestEmbeddedFileProvider(this.GetType().Assembly, this.Root);

            options.FileProvider = new CompositeFileProvider(options.FileProvider, fileProvider);
        }
    }
}
