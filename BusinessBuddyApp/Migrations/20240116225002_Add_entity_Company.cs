using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBuddyApp.Migrations
{
    public partial class Add_entity_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Users",
                type: "int",
                nullable: true);

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
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Mugs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Clothes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "IdCompany",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Addresses",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hasSubscription = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CompanyId",
                table: "Addresses",
                column: "CompanyId",
                unique: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Companies_CompanyId",
                table: "Addresses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Companies_CompanyId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CompanyId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Others");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Mugs");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "IdCompany",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
