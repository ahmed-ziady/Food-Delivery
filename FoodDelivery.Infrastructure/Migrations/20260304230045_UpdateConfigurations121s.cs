using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurations121s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Users_RestaurantId",
                table: "Menus");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Users_RestaurantId",
                table: "Menus",
                column: "RestaurantId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Users_RestaurantId",
                table: "Menus");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Users_RestaurantId",
                table: "Menus",
                column: "RestaurantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
