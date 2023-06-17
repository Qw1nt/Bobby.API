using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Upd4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnityGameObjects_GameWorlds_UnityWorldId",
                table: "UnityGameObjects");

            migrationBuilder.DropIndex(
                name: "IX_UnityGameObjects_UnityWorldId",
                table: "UnityGameObjects");

            migrationBuilder.DropColumn(
                name: "UnityWorldId",
                table: "UnityGameObjects");

            migrationBuilder.CreateTable(
                name: "UnityWorldGameObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReferenceId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<float>(type: "real", nullable: false),
                    Y = table.Column<float>(type: "real", nullable: false),
                    Z = table.Column<float>(type: "real", nullable: false),
                    UnityWorldId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnityWorldGameObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnityWorldGameObject_GameWorlds_UnityWorldId",
                        column: x => x.UnityWorldId,
                        principalTable: "GameWorlds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnityWorldGameObject_UnityGameObjects_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "UnityGameObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnityWorldGameObject_ReferenceId",
                table: "UnityWorldGameObject",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_UnityWorldGameObject_UnityWorldId",
                table: "UnityWorldGameObject",
                column: "UnityWorldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnityWorldGameObject");

            migrationBuilder.AddColumn<int>(
                name: "UnityWorldId",
                table: "UnityGameObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnityGameObjects_UnityWorldId",
                table: "UnityGameObjects",
                column: "UnityWorldId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnityGameObjects_GameWorlds_UnityWorldId",
                table: "UnityGameObjects",
                column: "UnityWorldId",
                principalTable: "GameWorlds",
                principalColumn: "Id");
        }
    }
}
