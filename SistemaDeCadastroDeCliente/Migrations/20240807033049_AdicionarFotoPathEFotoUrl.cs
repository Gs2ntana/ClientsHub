using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaDeCadastroDeCliente.Migrations
{
    public partial class AdicionarFotoPathEFotoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPath",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPath",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Fornecedores");
        }
    }
}
