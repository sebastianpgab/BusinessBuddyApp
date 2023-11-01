using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBuddyApp.Migrations
{
    public partial class rename_Name_on_ProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Products",
                newName: "Name");
        }
    }
}
