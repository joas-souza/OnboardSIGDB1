using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class MudandoRelacionamentoFuncionarioCargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Funcionarios",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CargosFuncionario",
                columns: table => new
                {
                    CargoId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    CargoId1 = table.Column<int>(nullable: true),
                    FuncionarioId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargosFuncionario", x => new { x.CargoId, x.FuncionarioId });
                    table.ForeignKey(
                        name: "FK_CargosFuncionario_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargosFuncionario_Cargos_CargoId1",
                        column: x => x.CargoId1,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargosFuncionario_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargosFuncionario_Funcionarios_FuncionarioId1",
                        column: x => x.FuncionarioId1,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_CargoId1",
                table: "CargosFuncionario",
                column: "CargoId1");

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_FuncionarioId",
                table: "CargosFuncionario",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CargosFuncionario_FuncionarioId1",
                table: "CargosFuncionario",
                column: "FuncionarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Empresas_EmpresaId",
                table: "Funcionarios");

            migrationBuilder.DropTable(
                name: "CargosFuncionario");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_EmpresaId",
                table: "Funcionarios");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Funcionarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "Funcionarios",
                type: "int",
                nullable: true);
        }
    }
}
