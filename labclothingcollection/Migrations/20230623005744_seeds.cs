using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace labclothingcollection.Migrations
{
    /// <inheritdoc />
    public partial class seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Identificador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Colecao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioIdentificador = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Orcamento = table.Column<int>(type: "int", nullable: false),
                    AnoLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colecao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colecao_Usuario_UsuarioIdentificador",
                        column: x => x.UsuarioIdentificador,
                        principalTable: "Usuario",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColecaoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Layout = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelo_Colecao_ColecaoId",
                        column: x => x.ColecaoId,
                        principalTable: "Colecao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Identificador", "Cpf", "DataNascimento", "Email", "Genero", "Nome", "Status", "Telefone", "Tipo" },
                values: new object[,]
                {
                    { 1, "123.456.789-00", new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste@teste.com", "masculino", "Lucas Silva", 0, "9512328", 2 },
                    { 2, "987.654.321-00", new DateTime(1930, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste2@example.com", "feminino", "Camila Z", 1, "987654321", 0 },
                    { 3, "456.789.123-00", new DateTime(1998, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste3@dominio.com", "feminino", "Maria das Dores", 1, "456789123", 1 },
                    { 4, "789.123.456-00", new DateTime(1987, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste4@example.com", "feminino", "Ana Rodrigues", 0, "321654987", 3 },
                    { 5, "321.654.987-00", new DateTime(1982, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste5@example.com", "Masculino", "Luiz Ferreira", 0, "321654987", 3 }
                });

            migrationBuilder.InsertData(
                table: "Colecao",
                columns: new[] { "Id", "AnoLancamento", "Estacao", "Marca", "Nome", "Orcamento", "Status", "UsuarioIdentificador" },
                values: new object[,]
                {
                    { 1, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Marca 1", "Nome 1", 1000, 0, 1 },
                    { 2, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Marca 2", "Nome 2", 1500, 0, 2 },
                    { 3, new DateTime(1993, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Marca 3", "Nome 3", 2000, 1, 1 },
                    { 4, new DateTime(1985, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marca 4", "Nome 4", 100, 0, 1 },
                    { 5, new DateTime(1970, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Marca 5", "Nome 5", 3500, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "Id", "ColecaoId", "Layout", "Nome", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, 0, "Nome 1", 2 },
                    { 2, 2, 2, "Nome 2", 4 },
                    { 3, 1, 1, "Nome 3", 6 },
                    { 4, 1, 0, "Nome 4", 5 },
                    { 5, 1, 2, "Nome 5", 0 },
                    { 6, 2, 1, "Nome 6", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colecao_Nome",
                table: "Colecao",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colecao_UsuarioIdentificador",
                table: "Colecao",
                column: "UsuarioIdentificador");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_ColecaoId",
                table: "Modelo",
                column: "ColecaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_Nome",
                table: "Modelo",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Cpf",
                table: "Usuario",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Colecao");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
