using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class add_shipments_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Order",
                table: "Shipments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Shipments");
        }
    }
}
