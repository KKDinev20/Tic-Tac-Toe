using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Board",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Board",
                table: "Games");
        }
    }
}
