using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moqas.Migrations
{
    public partial class CustomerAddBrowserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenExpores",
                table: "Customers",
                newName: "ResetTokenExpires");

            migrationBuilder.AddColumn<string>(
                name: "BrowserToken",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrowserToken",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ResetTokenExpires",
                table: "Customers",
                newName: "ResetTokenExpores");
        }
    }
}
