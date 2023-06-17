using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Upd5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnityWorldGameObject_UnityGameObjects_ReferenceId",
                table: "UnityWorldGameObject");

            migrationBuilder.DropIndex(
                name: "IX_UnityWorldGameObject_ReferenceId",
                table: "UnityWorldGameObject");

            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "UnityWorldGameObject",
                newName: "UnityGameObjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnityGameObjectId",
                table: "UnityWorldGameObject",
                newName: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_UnityWorldGameObject_ReferenceId",
                table: "UnityWorldGameObject",
                column: "ReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnityWorldGameObject_UnityGameObjects_ReferenceId",
                table: "UnityWorldGameObject",
                column: "ReferenceId",
                principalTable: "UnityGameObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
