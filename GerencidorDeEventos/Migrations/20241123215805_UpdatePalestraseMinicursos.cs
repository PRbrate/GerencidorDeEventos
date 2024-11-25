using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerencidorDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePalestraseMinicursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Minicursos");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Minicursos");

            migrationBuilder.CreateTable(
                name: "InscricaoMinicurso",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    MinicursoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricaoMinicurso", x => new { x.UsuarioId, x.MinicursoId });
                    table.ForeignKey(
                        name: "FK_InscricaoMinicurso_Minicursos_MinicursoId",
                        column: x => x.MinicursoId,
                        principalTable: "Minicursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InscricaoMinicurso_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InscricaoPalestra",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    PalestraId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscricaoPalestra", x => new { x.UsuarioId, x.PalestraId });
                    table.ForeignKey(
                        name: "FK_InscricaoPalestra_Palestras_PalestraId",
                        column: x => x.PalestraId,
                        principalTable: "Palestras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InscricaoPalestra_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Palestras_EventoId",
                table: "Palestras",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Minicursos_EventoId",
                table: "Minicursos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_InscricaoMinicurso_MinicursoId",
                table: "InscricaoMinicurso",
                column: "MinicursoId");

            migrationBuilder.CreateIndex(
                name: "IX_InscricaoPalestra_PalestraId",
                table: "InscricaoPalestra",
                column: "PalestraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Minicursos_Eventos_EventoId",
                table: "Minicursos",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Palestras_Eventos_EventoId",
                table: "Palestras",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Minicursos_Eventos_EventoId",
                table: "Minicursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Palestras_Eventos_EventoId",
                table: "Palestras");

            migrationBuilder.DropTable(
                name: "InscricaoMinicurso");

            migrationBuilder.DropTable(
                name: "InscricaoPalestra");

            migrationBuilder.DropIndex(
                name: "IX_Palestras_EventoId",
                table: "Palestras");

            migrationBuilder.DropIndex(
                name: "IX_Minicursos_EventoId",
                table: "Minicursos");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFim",
                table: "Minicursos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Minicursos",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
