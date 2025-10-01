using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhasTarefasAPI.Migrations
{
    public partial class AddFKToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriado",
                table: "Token",
                newName: "Criado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Criado",
                table: "Token",
                newName: "DataCriado");
        }
    }
}
