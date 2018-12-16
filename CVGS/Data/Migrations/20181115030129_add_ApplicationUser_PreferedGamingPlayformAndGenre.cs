using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class add_ApplicationUser_PreferedGamingPlayformAndGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferedGamingGenre",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferedGamingPlateForm",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferedGamingGenre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreferedGamingPlateForm",
                table: "AspNetUsers");
        }
    }
}
