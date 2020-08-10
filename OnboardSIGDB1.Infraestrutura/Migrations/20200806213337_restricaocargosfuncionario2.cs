using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class restricaocargosfuncionario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId",
                table: "CargosFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId",
                table: "CargosFuncionario",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId",
                table: "CargosFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId",
                table: "CargosFuncionario",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
