using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class changes_to_Orders_and_Shipments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_ApplicationUserId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ApplicationUserId",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Shipments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Shipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShipmentAddress",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "hasBeenApproved",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Orders_OrderId",
                table: "Shipments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Orders_OrderId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ShipmentAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "hasBeenApproved",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Shipments",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ApplicationUserId",
                table: "Shipments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_ApplicationUserId",
                table: "Shipments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
