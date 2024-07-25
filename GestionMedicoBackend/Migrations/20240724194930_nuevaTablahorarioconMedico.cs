using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionMedicoBackend.Migrations
{
    /// <inheritdoc />
    public partial class nuevaTablahorarioconMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "Medics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrada = table.Column<TimeOnly>(type: "time", nullable: false),
                    Salida = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medics_HorarioId",
                table: "Medics",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medics_Horarios_HorarioId",
                table: "Medics",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medics_Horarios_HorarioId",
                table: "Medics");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropIndex(
                name: "IX_Medics_HorarioId",
                table: "Medics");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "Medics");
        }
    }
}
