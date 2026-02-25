using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniTrello.Migrations
{
    /// <inheritdoc />
    public partial class newfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Cards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
