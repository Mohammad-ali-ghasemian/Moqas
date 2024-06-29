using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moqas.Migrations
{
    public partial class InsertConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ConfigPasswordHash",
                table: "Customers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "ConfigPasswordSalt",
                table: "Customers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ConfigUsername",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigPasswordHash",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ConfigPasswordSalt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ConfigUsername",
                table: "Customers");
        }
    }
}
