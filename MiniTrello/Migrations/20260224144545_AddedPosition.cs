using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniTrello.Migrations
{
    /// <inheritdoc />
    public partial class AddedPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_BoardLists_BoardListId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "BoardListId",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_BoardLists_BoardListId",
                table: "Cards",
                column: "BoardListId",
                principalTable: "BoardLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_BoardLists_BoardListId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "BoardListId",
                table: "Cards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_BoardLists_BoardListId",
                table: "Cards",
                column: "BoardListId",
                principalTable: "BoardLists",
                principalColumn: "Id");
        }
    }
}
