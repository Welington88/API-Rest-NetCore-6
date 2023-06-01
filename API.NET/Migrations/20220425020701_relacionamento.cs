using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.NET6.Migrations
{
    public partial class relacionamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_IdCargo",
                table: "Colaborador",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_IdSetor",
                table: "Colaborador",
                column: "IdSetor");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_IdSetor",
                table: "Cargo",
                column: "IdSetor");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Setor_IdSetor",
                table: "Cargo",
                column: "IdSetor",
                principalTable: "Setor",
                principalColumn: "IdSetor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Cargo_IdCargo",
                table: "Colaborador",
                column: "IdCargo",
                principalTable: "Cargo",
                principalColumn: "IdCargo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Setor_IdSetor",
                table: "Colaborador",
                column: "IdSetor",
                principalTable: "Setor",
                principalColumn: "IdSetor",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Setor_IdSetor",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Cargo_IdCargo",
                table: "Colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Setor_IdSetor",
                table: "Colaborador");

            migrationBuilder.DropIndex(
                name: "IX_Colaborador_IdCargo",
                table: "Colaborador");

            migrationBuilder.DropIndex(
                name: "IX_Colaborador_IdSetor",
                table: "Colaborador");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_IdSetor",
                table: "Cargo");
        }
    }
}
