using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerencidorDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class relacionamentosMinicurso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InscricaoMinicurso_Minicursos_MinicursoId",
                table: "InscricaoMinicurso");

            migrationBuilder.DropForeignKey(
                name: "FK_InscricaoMinicurso_Usuarios_UsuarioId",
                table: "InscricaoMinicurso");

            migrationBuilder.DropForeignKey(
                name: "FK_InscricaoPalestra_Palestras_PalestraId",
                table: "InscricaoPalestra");

            migrationBuilder.DropForeignKey(
                name: "FK_InscricaoPalestra_Usuarios_UsuarioId",
                table: "InscricaoPalestra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InscricaoPalestra",
                table: "InscricaoPalestra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InscricaoMinicurso",
                table: "InscricaoMinicurso");

            migrationBuilder.RenameTable(
                name: "InscricaoPalestra",
                newName: "InscricoesPalestra");

            migrationBuilder.RenameTable(
                name: "InscricaoMinicurso",
                newName: "InscricoesMinicurso");

            migrationBuilder.RenameIndex(
                name: "IX_InscricaoPalestra_PalestraId",
                table: "InscricoesPalestra",
                newName: "IX_InscricoesPalestra_PalestraId");

            migrationBuilder.RenameIndex(
                name: "IX_InscricaoMinicurso_MinicursoId",
                table: "InscricoesMinicurso",
                newName: "IX_InscricoesMinicurso_MinicursoId");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "InscricoesMinicurso",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "InscricoesMinicurso",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InscricoesPalestra",
                table: "InscricoesPalestra",
                columns: new[] { "UsuarioId", "PalestraId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_InscricoesMinicurso",
                table: "InscricoesMinicurso",
                columns: new[] { "UsuarioId", "MinicursoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InscricoesMinicurso_Minicursos_MinicursoId",
                table: "InscricoesMinicurso",
                column: "MinicursoId",
                principalTable: "Minicursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InscricoesMinicurso_Usuarios_UsuarioId",
                table: "InscricoesMinicurso",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InscricoesPalestra_Palestras_PalestraId",
                table: "InscricoesPalestra",
                column: "PalestraId",
                principalTable: "Palestras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InscricoesPalestra_Usuarios_UsuarioId",
                table: "InscricoesPalestra",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InscricoesMinicurso_Minicursos_MinicursoId",
                table: "InscricoesMinicurso");

            migrationBuilder.DropForeignKey(
                name: "FK_InscricoesMinicurso_Usuarios_UsuarioId",
                table: "InscricoesMinicurso");

            migrationBuilder.DropForeignKey(
                name: "FK_InscricoesPalestra_Palestras_PalestraId",
                table: "InscricoesPalestra");

            migrationBuilder.DropForeignKey(
                name: "FK_InscricoesPalestra_Usuarios_UsuarioId",
                table: "InscricoesPalestra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InscricoesPalestra",
                table: "InscricoesPalestra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InscricoesMinicurso",
                table: "InscricoesMinicurso");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "InscricoesMinicurso");

            migrationBuilder.DropColumn(
                name: "email",
                table: "InscricoesMinicurso");

            migrationBuilder.RenameTable(
                name: "InscricoesPalestra",
                newName: "InscricaoPalestra");

            migrationBuilder.RenameTable(
                name: "InscricoesMinicurso",
                newName: "InscricaoMinicurso");

            migrationBuilder.RenameIndex(
                name: "IX_InscricoesPalestra_PalestraId",
                table: "InscricaoPalestra",
                newName: "IX_InscricaoPalestra_PalestraId");

            migrationBuilder.RenameIndex(
                name: "IX_InscricoesMinicurso_MinicursoId",
                table: "InscricaoMinicurso",
                newName: "IX_InscricaoMinicurso_MinicursoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InscricaoPalestra",
                table: "InscricaoPalestra",
                columns: new[] { "UsuarioId", "PalestraId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_InscricaoMinicurso",
                table: "InscricaoMinicurso",
                columns: new[] { "UsuarioId", "MinicursoId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InscricaoMinicurso_Minicursos_MinicursoId",
                table: "InscricaoMinicurso",
                column: "MinicursoId",
                principalTable: "Minicursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InscricaoMinicurso_Usuarios_UsuarioId",
                table: "InscricaoMinicurso",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InscricaoPalestra_Palestras_PalestraId",
                table: "InscricaoPalestra",
                column: "PalestraId",
                principalTable: "Palestras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InscricaoPalestra_Usuarios_UsuarioId",
                table: "InscricaoPalestra",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
