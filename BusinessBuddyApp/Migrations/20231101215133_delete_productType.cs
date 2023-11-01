using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBuddyApp.Migrations
{
    public partial class delete_productType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Others");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Mugs");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Clothes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Others",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Mugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Clothes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
