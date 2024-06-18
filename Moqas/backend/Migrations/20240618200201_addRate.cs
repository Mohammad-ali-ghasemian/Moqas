using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moqas.Migrations
{
    public partial class addRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Chats");
        }
    }
}
