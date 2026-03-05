using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIngrients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemIngredients_MenuItems_MenuItemId",
                table: "MenuItemIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemIngredients",
                table: "MenuItemIngredients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MenuItemIngredients");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MenuItemIngredients");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MenuItemIngredients");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IngredientId",
                table: "MenuItemIngredients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemIngredients",
                table: "MenuItemIngredients",
                columns: new[] { "MenuItemId", "IngredientId" });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemIngredients_IngredientId",
                table: "MenuItemIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MenuItemId",
                table: "Ingredients",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemIngredients_Ingredients_IngredientId",
                table: "MenuItemIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemIngredients_MenuItems_MenuItemId",
                table: "MenuItemIngredients",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemIngredients_Ingredients_IngredientId",
                table: "MenuItemIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemIngredients_MenuItems_MenuItemId",
                table: "MenuItemIngredients");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItemIngredients",
                table: "MenuItemIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemIngredients_IngredientId",
                table: "MenuItemIngredients");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "MenuItemIngredients");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MenuItemIngredients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MenuItemIngredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "MenuItemIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItemIngredients",
                table: "MenuItemIngredients",
                columns: new[] { "MenuItemId", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemIngredients_MenuItems_MenuItemId",
                table: "MenuItemIngredients",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
