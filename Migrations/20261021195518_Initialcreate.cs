using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestorTareas.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nombre" },
                values: new object[,]
                {
                    { 1, "admin@workspace.com", "Administrador Sistema" },
                    { 2, "ana.dev@workspace.com", "Ana Gómez (Dev)" }
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "Descripcion", "Estado", "FechaVencimiento", "Titulo", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Crear el AppDbContext y capa servicios.", "Completada", new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diseñar Arquitectura base", 1 },
                    { 2, "Implementar CSS Grid para las 3 columnas.", "En Progreso", new DateTime(2026, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desarrollar Frontend Tablero", 2 },
                    { 3, "Cambiar los etados de forma asíncrona.", "Pendiente", new DateTime(2026, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Integrar Fetch API", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
