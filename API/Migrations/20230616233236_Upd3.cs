using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Upd3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnityGameObjects_GameWorlds_GameWorldId",
                table: "UnityGameObjects");

            migrationBuilder.RenameColumn(
                name: "GameWorldId",
                table: "UnityGameObjects",
                newName: "UnityWorldId");

            migrationBuilder.RenameIndex(
                name: "IX_UnityGameObjects_GameWorldId",
                table: "UnityGameObjects",
                newName: "IX_UnityGameObjects_UnityWorldId");

            migrationBuilder.AddColumn<int>(
                name: "SceneIndex",
                table: "GameWorlds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UnityGameObjects_GameWorlds_UnityWorldId",
                table: "UnityGameObjects",
                column: "UnityWorldId",
                principalTable: "GameWorlds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnityGameObjects_GameWorlds_UnityWorldId",
                table: "UnityGameObjects");

            migrationBuilder.DropColumn(
                name: "SceneIndex",
                table: "GameWorlds");

            migrationBuilder.RenameColumn(
                name: "UnityWorldId",
                table: "UnityGameObjects",
                newName: "GameWorldId");

            migrationBuilder.RenameIndex(
                name: "IX_UnityGameObjects_UnityWorldId",
                table: "UnityGameObjects",
                newName: "IX_UnityGameObjects_GameWorldId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnityGameObjects_GameWorlds_GameWorldId",
                table: "UnityGameObjects",
                column: "GameWorldId",
                principalTable: "GameWorlds",
                principalColumn: "Id");
        }
    }
}
