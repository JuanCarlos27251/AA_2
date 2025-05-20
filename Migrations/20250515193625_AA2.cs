using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AA2.Migrations
{
    /// <inheritdoc />
    public partial class AA2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdMedico = table.Column<int>(type: "int", nullable: false),
                    FechaCita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Confirmada = table.Column<bool>(type: "bit", nullable: false),
                    NombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreMedico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Citas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Citas",
                columns: new[] { "Id", "Confirmada", "FechaCita", "IdMedico", "IdUsuario", "MedicoId", "Motivo", "NombreMedico", "NombrePaciente", "UsuarioId" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2025, 5, 14, 20, 20, 0, 0, DateTimeKind.Unspecified), 3, 4, null, "dolor", "Dr. Fernandez Santos", "juan", null },
                    { 2, false, new DateTime(2025, 5, 16, 10, 32, 0, 0, DateTimeKind.Unspecified), 2, 2, null, "corazon", "Dra. Martinez Ruiz", "Pepe", null },
                    { 3, false, new DateTime(2025, 6, 20, 20, 1, 0, 0, DateTimeKind.Unspecified), 6, 5, null, "garganta", "lili", "kaka", null },
                    { 4, false, new DateTime(2025, 5, 20, 12, 2, 0, 0, DateTimeKind.Unspecified), 4, 6, null, "ere", "pepe", "ere", null },
                    { 5, false, new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 8, 7, null, "qq", "qq", "qq", null }
                });

            migrationBuilder.InsertData(
                table: "Medicos",
                columns: new[] { "Id", "Disponible", "Email", "Especialidad", "FechaAlta", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, true, "garcia@citasmedicas.com", "Medicina General", new DateTime(2024, 11, 13, 15, 12, 36, 176, DateTimeKind.Local).AddTicks(7151), "Dr. Garcia Lopez", "600123456" },
                    { 2, true, "martinez@citasmedicas.com", "Cardiologia", new DateTime(2025, 2, 13, 15, 12, 36, 187, DateTimeKind.Local).AddTicks(2052), "Dra. Martinez Ruiz", "600789012" },
                    { 3, true, "fernandez@citasmedicas.com", "Pediatria", new DateTime(2025, 4, 13, 15, 12, 36, 187, DateTimeKind.Local).AddTicks(2084), "Dr. Fernandez Santos", "600345678" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contrasena", "Email", "EstaActivo", "FechaRegistro", "Nombre", "Rol" },
                values: new object[,]
                {
                    { 1, "admin123", "admin@gmail.com", true, new DateTime(2025, 5, 13, 15, 12, 36, 290, DateTimeKind.Local).AddTicks(5962), "admin", "Admin" },
                    { 2, "123456", "pepe@gmail.com", true, new DateTime(2025, 5, 13, 15, 12, 36, 290, DateTimeKind.Local).AddTicks(6353), "Pepe", "Paciente" },
                    { 3, "123456", "maria@gmail.com", true, new DateTime(2025, 5, 13, 15, 12, 36, 290, DateTimeKind.Local).AddTicks(6357), "Maria", "Paciente" },
                    { 4, "123456789", "juan@gmail.com", true, new DateTime(2025, 5, 13, 15, 19, 7, 300, DateTimeKind.Local).AddTicks(8026), "juan", "Paciente" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_MedicoId",
                table: "Citas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_UsuarioId",
                table: "Citas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
