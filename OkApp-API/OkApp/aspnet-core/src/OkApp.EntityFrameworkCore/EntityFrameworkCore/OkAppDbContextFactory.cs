using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OkApp.Configuration;
using OkApp.Web;

namespace OkApp.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class OkAppDbContextFactory : IDesignTimeDbContextFactory<OkAppDbContext>
    {
        public OkAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OkAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            OkAppDbContextConfigurer.Configure(builder, configuration.GetConnectionString(OkAppConsts.ConnectionStringName));

            return new OkAppDbContext(builder.Options);
        }
    }
}

