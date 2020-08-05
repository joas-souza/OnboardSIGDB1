using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class CriandoIdCargoFuncionario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CascadeMode",
                table: "CargosFuncionario");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CargosFuncionario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CascadeMode",
                table: "CargosFuncionario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CargosFuncionario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
