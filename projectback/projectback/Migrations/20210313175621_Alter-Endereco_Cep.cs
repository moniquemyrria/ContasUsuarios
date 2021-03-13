using Microsoft.EntityFrameworkCore.Migrations;

namespace projectback.Migrations
{
    public partial class AlterEndereco_Cep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cep",
                table: "Enderecos",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cep",
                table: "Enderecos");
        }
    }
}
