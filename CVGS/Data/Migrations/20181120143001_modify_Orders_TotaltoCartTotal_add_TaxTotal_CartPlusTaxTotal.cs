using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class modify_Orders_TotaltoCartTotal_add_TaxTotal_CartPlusTaxTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "TaxTotal");

            migrationBuilder.AddColumn<double>(
                name: "CartPlusTaxTotal",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CartTotal",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartPlusTaxTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CartTotal",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TaxTotal",
                table: "Orders",
                newName: "Total");
        }
    }
}
