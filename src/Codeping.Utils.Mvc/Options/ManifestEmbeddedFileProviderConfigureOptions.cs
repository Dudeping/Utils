using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Codeping.Utils.Mvc
{
    /// <summary>
    /// 嵌入资源提供程序配置选项, 将指定嵌入的资源以作为文件提供程序
    /// 使用：services.ConfigureOptions<T>();
    /// 注意：一定要在 AddMvc 之前注入!
    /// </summary>
    public abstract class ManifestEmbeddedFileProviderConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        protected readonly IHostingEnvironment _host;

        public ManifestEmbeddedFileProviderConfigureOptions(IHostingEnvironment host)
        {
            _host = host;
        }

        /// <summary>
        /// 资源所在根路径
        /// </summary>
        protected abstract string Root { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            // see https://www.cnblogs.com/sheng-jie/p/9165547.html

            options = options ?? throw new ArgumentNullException(nameof(options));

            if (options.FileProvider == null && _host.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("找不到文件提供程序!");
            }

            options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();

            options.FileProvider ??= _host.WebRootFileProvider;

            var fileProvider = new ManifestEmbeddedFileProvider(this.GetType().Assembly, this.Root);

            options.FileProvider = new CompositeFileProvider(options.FileProvider, fileProvider);
        }
    }
}
