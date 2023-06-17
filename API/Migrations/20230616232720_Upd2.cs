using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Upd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameWorlds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameWorlds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnityGameObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdInUnity = table.Column<int>(type: "integer", nullable: false),
                    GameWorldId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnityGameObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnityGameObjects_GameWorlds_GameWorldId",
                        column: x => x.GameWorldId,
                        principalTable: "GameWorlds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnityGameObjects_GameWorldId",
                table: "UnityGameObjects",
                column: "GameWorldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnityGameObjects");

            migrationBuilder.DropTable(
                name: "GameWorlds");
        }
    }
}
