using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class add_Games_isFree_isDownloadable_ExeLink_RemoveEntryDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "ExeLink",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDownloadable",
                table: "Games",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isFree",
                table: "Games",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExeLink",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "isDownloadable",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "isFree",
                table: "Games");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
