using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GerencidorDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class MudandoEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_InscricaoEventos_InscricaoEventoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "InscricaoEventos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_InscricaoEventoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "InscricaoEventoId",
                table: "Usuarios");

            migrationBuilder.CreateTable(
                name: "Inscricoes",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "integer", nullable: false),
                    UsuariosInscritosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricoes", x => new { x.EventoId, x.UsuariosInscritosId });
                    table.ForeignKey(
                        name: "FK_Inscricoes_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscricoes_Usuarios_UsuariosInscritosId",
                        column: x => x.UsuariosInscritosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricoes_UsuariosInscritosId",
                table: "Inscricoes",
                column: "UsuariosInscritosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricoes");

            migrationBuilder.AddColumn<int>(
                name: "InscricaoEventoId",
                table: "Usuarios",
                type: "integer",
                nullable: true);

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
    }
}
