using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using OkApp.Authorization;

namespace OkApp
{
    [DependsOn(
        typeof(OkAppCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class OkAppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<OkAppAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(OkAppApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}

