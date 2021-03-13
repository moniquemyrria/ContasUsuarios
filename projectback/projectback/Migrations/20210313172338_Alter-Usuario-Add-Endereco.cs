using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projectback.Migrations
{
    public partial class AlterUsuarioAddEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "Usuarios",
                maxLength: 13,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataNascimento",
                table: "Usuarios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<long>(nullable: false),
                    logradouro = table.Column<string>(maxLength: 80, nullable: true),
                    bairro = table.Column<string>(maxLength: 80, nullable: true),
                    localidade = table.Column<string>(maxLength: 50, nullable: true),
                    uf = table.Column<string>(maxLength: 2, nullable: true),
                    tipo = table.Column<string>(maxLength: 20, nullable: true),
                    numero = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_idUsuario",
                table: "Enderecos",
                column: "idUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "dataNascimento",
                table: "Usuarios");
        }
    }
}
