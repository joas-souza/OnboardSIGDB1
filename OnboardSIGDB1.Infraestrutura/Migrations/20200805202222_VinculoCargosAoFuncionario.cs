using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class VinculoCargosAoFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Cargos_CargoId",
                table: "CargosFuncionario");

            migrationBuilder.DropForeignKey(
                name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                table: "CargosFuncionario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CargosFuncionario",
                table: "CargosFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_CargosFuncionario_CargoId",
                table: "CargosFuncionario");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CargosFuncionario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CargosFuncionario",
                table: "CargosFuncionario",
                columns: new[] { "CargoId", "FuncionarioId" });

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_CargosFuncionario",
                table: "CargosFuncionario");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CargosFuncionario",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CargosFuncionario",
                table: "CargosFuncionario",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_CargoId",
                table: "CargosFuncionario",
                column: "CargoId");

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
