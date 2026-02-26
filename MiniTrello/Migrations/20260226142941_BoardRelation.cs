using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniTrello.Migrations
{
    /// <inheritdoc />
    public partial class BoardRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Boards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_UserId",
                table: "Boards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_UserId",
                table: "Boards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_UserId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_UserId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Boards");
        }
    }
}
