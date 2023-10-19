using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBuddyApp.Migrations
{
    public partial class Add_composition_relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Others",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Mugs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Clothes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Others_ProductId",
                table: "Others",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mugs_ProductId",
                table: "Mugs",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_ProductId",
                table: "Clothes",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Products_ProductId",
                table: "Clothes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mugs_Products_ProductId",
                table: "Mugs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Others_Products_ProductId",
                table: "Others",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Products_ProductId",
                table: "Clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mugs_Products_ProductId",
                table: "Mugs");

            migrationBuilder.DropForeignKey(
                name: "FK_Others_Products_ProductId",
                table: "Others");

            migrationBuilder.DropIndex(
                name: "IX_Others_ProductId",
                table: "Others");

            migrationBuilder.DropIndex(
                name: "IX_Mugs_ProductId",
                table: "Mugs");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_ProductId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Others");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Mugs");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Clothes");
        }
    }
}
