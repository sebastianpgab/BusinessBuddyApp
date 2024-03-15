using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBuddyApp.Migrations
{
    public partial class Delete_companyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Others");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Mugs");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Clothes");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "Addresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Others",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Mugs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Clothes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderDetailId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
