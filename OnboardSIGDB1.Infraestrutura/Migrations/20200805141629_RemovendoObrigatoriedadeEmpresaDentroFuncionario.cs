using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class RemovendoObrigatoriedadeEmpresaDentroFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Funcionarios",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
