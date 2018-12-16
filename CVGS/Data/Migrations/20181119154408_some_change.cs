using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class some_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_ApplicationUserId",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.RenameTable(
                name: "ShippingAddresses",
                newName: "ShippingMailingAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddresses_ApplicationUserId",
                table: "ShippingMailingAddresses",
                newName: "IX_ShippingMailingAddresses_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMailingAddresses",
                table: "ShippingMailingAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingMailingAddresses_AspNetUsers_ApplicationUserId",
                table: "ShippingMailingAddresses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingMailingAddresses_AspNetUsers_ApplicationUserId",
                table: "ShippingMailingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMailingAddresses",
                table: "ShippingMailingAddresses");

            migrationBuilder.RenameTable(
                name: "ShippingMailingAddresses",
                newName: "ShippingAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingMailingAddresses_ApplicationUserId",
                table: "ShippingAddresses",
                newName: "IX_ShippingAddresses_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_ApplicationUserId",
                table: "ShippingAddresses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
