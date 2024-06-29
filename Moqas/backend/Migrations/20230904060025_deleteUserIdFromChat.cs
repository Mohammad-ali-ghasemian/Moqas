using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moqas.Migrations
{
    public partial class deleteUserIdFromChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
