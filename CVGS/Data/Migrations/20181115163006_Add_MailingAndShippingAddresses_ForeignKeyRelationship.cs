using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class Add_MailingAndShippingAddresses_ForeignKeyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ShippingAddresses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "MailingAddresses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_ApplicationUserId",
                table: "ShippingAddresses",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MailingAddresses_ApplicationUserId",
                table: "MailingAddresses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingAddresses_AspNetUsers_ApplicationUserId",
                table: "MailingAddresses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_ApplicationUserId",
                table: "ShippingAddresses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingAddresses_AspNetUsers_ApplicationUserId",
                table: "MailingAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_ApplicationUserId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_ApplicationUserId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_MailingAddresses_ApplicationUserId",
                table: "MailingAddresses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "MailingAddresses");
        }
    }
}
