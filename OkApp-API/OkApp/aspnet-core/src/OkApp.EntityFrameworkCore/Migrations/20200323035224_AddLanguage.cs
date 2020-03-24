using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OkApp.Migrations
{
    public partial class AddLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AbpUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "AbpUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "AbpUsers");
        }
    }
}
