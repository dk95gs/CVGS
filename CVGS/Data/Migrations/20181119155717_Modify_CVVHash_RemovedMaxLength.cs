using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class Modify_CVVHash_RemovedMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CVVHash",
                table: "CreditCards",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 3,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CVVHash",
                table: "CreditCards",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
