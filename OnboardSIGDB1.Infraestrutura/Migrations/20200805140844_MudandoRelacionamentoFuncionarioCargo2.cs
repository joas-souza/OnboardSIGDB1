using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class MudandoRelacionamentoFuncionarioCargo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId1",
                table: "CargosFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId1",
                table: "CargosFuncionario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CargosFuncionario",
                table: "CargosFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_CargosFuncionario_CargoId1",
                table: "CargosFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_CargosFuncionario_FuncionarioId1",
                table: "CargosFuncionario");

            migrationBuilder.DropColumn(
                name: "CargoId1",
                table: "CargosFuncionario");

            migrationBuilder.DropColumn(
                name: "FuncionarioId1",
                table: "CargosFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_CargoId",
                table: "CargosFuncionario",
                column: "CargoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CargosFuncionario_CargoId",
                table: "CargosFuncionario");

            migrationBuilder.AddColumn<int>(
                name: "CargoId1",
                table: "CargosFuncionario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId1",
                table: "CargosFuncionario",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CargosFuncionario",
                table: "CargosFuncionario",
                columns: new[] { "CargoId", "FuncionarioId" });

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_CargoId1",
                table: "CargosFuncionario",
                column: "CargoId1");

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_FuncionarioId1",
                table: "CargosFuncionario",
                column: "FuncionarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId1",
                table: "CargosFuncionario",
                column: "CargoId1",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId1",
                table: "CargosFuncionario",
                column: "FuncionarioId1",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
