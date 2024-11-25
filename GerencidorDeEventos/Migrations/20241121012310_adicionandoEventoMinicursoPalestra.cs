using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GerencidorDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoEventoMinicursoPalestra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Usuarios",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "InscricaoEventoId",
                table: "Usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(300)", unicode: false, maxLength: 300, nullable: false),
                    NomeResponsavel = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    CpfResponsavel = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    NumVagas = table.Column<int>(type: "integer", nullable: false),
                    DataLimiteInscricao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InscricaoEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricaoEventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Minicursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(300)", unicode: false, maxLength: 300, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Palestrante = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    CurriculoPalestrante = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    QuantidadeDeVagas = table.Column<int>(type: "integer", nullable: false),
                    LimiteInscricao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minicursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palestras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(300)", unicode: false, maxLength: 300, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Palestrante = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    CurriculoPalestrante = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestras", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_InscricaoEventoId",
                table: "Usuarios",
                column: "InscricaoEventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_InscricaoEventos_InscricaoEventoId",
                table: "Usuarios",
                column: "InscricaoEventoId",
                principalTable: "InscricaoEventos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_InscricaoEventos_InscricaoEventoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "InscricaoEventos");

            migrationBuilder.DropTable(
                name: "Minicursos");

            migrationBuilder.DropTable(
                name: "Palestras");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_InscricaoEventoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "InscricaoEventoId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Usuarios",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(14)",
                oldMaxLength: 14);
        }
    }
}
