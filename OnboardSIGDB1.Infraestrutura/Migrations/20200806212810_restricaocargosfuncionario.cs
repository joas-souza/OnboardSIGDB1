using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class restricaocargosfuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario");

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
