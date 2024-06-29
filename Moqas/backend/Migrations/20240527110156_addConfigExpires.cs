using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moqas.Migrations
{
    public partial class addConfigExpires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfigExpires",
                table: "Customers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigExpires",
                table: "Customers");
        }
    }
}
