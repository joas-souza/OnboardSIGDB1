using Microsoft.EntityFrameworkCore.Migrations;

namespace OnboardSIGDB1.Infraestrutura.Migrations
{
    public partial class CriandoIdCargoFuncionario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CascadeMode",
                table: "CargosFuncionario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CargosFuncionario",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CascadeMode",
                table: "CargosFuncionario");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CargosFuncionario");
        }
    }
}
