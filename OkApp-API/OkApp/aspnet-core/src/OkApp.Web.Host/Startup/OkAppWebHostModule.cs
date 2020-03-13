using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using OkApp.Configuration;

namespace OkApp.Web.Host.Startup
{
    [DependsOn(
       typeof(OkAppWebCoreModule))]
    public class OkAppWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public OkAppWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(OkAppWebHostModule).GetAssembly());
        }
    }
}

