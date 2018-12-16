using Microsoft.EntityFrameworkCore.Migrations;

namespace CVGS.Data.Migrations
{
    public partial class add_Carts_FriendsCartItems_Modify_CartItemsToMyCartItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartItems",
                table: "Carts",
                newName: "MyCartItems");

            migrationBuilder.AddColumn<string>(
                name: "FriendsCartItems",
                table: "Carts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendsCartItems",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "MyCartItems",
                table: "Carts",
                newName: "CartItems");
        }
    }
}
