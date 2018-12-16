using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class addEventsToDatabase_StartingTime_EndingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndingTime",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartingTime",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartingTime",
                table: "Events");
        }
    }
}
