using Microsoft.EntityFrameworkCore.Migrations;

namespace OkApp.Migrations
{
    public partial class InitialDbCollation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // DB NAME MUST BE SPECIFY HERE
            migrationBuilder.Sql("ALTER DATABASE OkAppDB CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

