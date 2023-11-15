using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBuddyApp.Migrations
{
    public partial class add_property_FinalAmount_to_OrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FinalAmount",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "OrderDetails");
        }
    }
}
