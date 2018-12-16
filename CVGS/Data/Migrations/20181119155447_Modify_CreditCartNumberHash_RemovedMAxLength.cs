using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class Modify_CreditCartNumberHash_RemovedMAxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreditCardNumberHash",
                table: "CreditCards",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreditCardNumberHash",
                table: "CreditCards",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
