using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace OkApp.EntityFrameworkCore
{
    public static class OkAppDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<OkAppDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<OkAppDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}

