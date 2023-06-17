using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameUnits",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameUnits_UserId",
                table: "GameUnits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameUnits_Users_UserId",
                table: "GameUnits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameUnits_Users_UserId",
                table: "GameUnits");

            migrationBuilder.DropIndex(
                name: "IX_GameUnits_UserId",
                table: "GameUnits");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameUnits");
        }
    }
}
