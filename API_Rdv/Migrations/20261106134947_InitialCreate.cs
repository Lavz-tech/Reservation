using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Rdv.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalleId = table.Column<int>(type: "integer", nullable: false),
                    CreneauId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateReservation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Capacite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Creneaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Debut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstReserve = table.Column<bool>(type: "boolean", nullable: false),
                    SalleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creneaux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creneaux_Salles_SalleId",
                        column: x => x.SalleId,
                        principalTable: "Salles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Salles",
                columns: new[] { "Id", "Capacite", "Nom" },
                values: new object[,]
                {
                    { 1, 10, "Salle A" },
                    { 2, 20, "Salle B" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nom" },
                values: new object[,]
                {
                    { 1, "alice@test.com", "Alice" },
                    { 2, "bob@test.com", "Bob" }
                });

            migrationBuilder.InsertData(
                table: "Creneaux",
                columns: new[] { "Id", "Debut", "EstReserve", "Fin", "SalleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 7, 9, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2026, 11, 7, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, new DateTime(2026, 11, 7, 10, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2026, 11, 7, 11, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 3, new DateTime(2026, 11, 7, 14, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2026, 11, 7, 15, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, new DateTime(2026, 11, 7, 9, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2026, 11, 7, 10, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 5, new DateTime(2026, 11, 7, 11, 0, 0, 0, DateTimeKind.Utc), false, new DateTime(2026, 11, 7, 12, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creneaux_SalleId",
                table: "Creneaux",
                column: "SalleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creneaux");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Salles");
        }
    }
}
