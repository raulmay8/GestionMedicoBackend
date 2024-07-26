using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionMedicoBackend.Migrations
{
    /// <inheritdoc />
    public partial class dos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialClinico_Patients_PatientId",
                table: "HistorialClinico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistorialClinico",
                table: "HistorialClinico");

            migrationBuilder.RenameTable(
                name: "HistorialClinico",
                newName: "HistorialClinicos");

            migrationBuilder.RenameIndex(
                name: "IX_HistorialClinico_PatientId",
                table: "HistorialClinicos",
                newName: "IX_HistorialClinicos_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistorialClinicos",
                table: "HistorialClinicos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialClinicos_Patients_PatientId",
                table: "HistorialClinicos",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorialClinicos_Patients_PatientId",
                table: "HistorialClinicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistorialClinicos",
                table: "HistorialClinicos");

            migrationBuilder.RenameTable(
                name: "HistorialClinicos",
                newName: "HistorialClinico");

            migrationBuilder.RenameIndex(
                name: "IX_HistorialClinicos_PatientId",
                table: "HistorialClinico",
                newName: "IX_HistorialClinico_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistorialClinico",
                table: "HistorialClinico",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorialClinico_Patients_PatientId",
                table: "HistorialClinico",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
