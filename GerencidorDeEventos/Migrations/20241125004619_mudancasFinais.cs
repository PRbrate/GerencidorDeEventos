using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerencidorDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class mudancasFinais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Palestras");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Palestras");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "InscricoesPalestra",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "InscricoesPalestra",
                type: "character varying(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "InscricoesPalestra");

            migrationBuilder.DropColumn(
                name: "email",
                table: "InscricoesPalestra");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFim",
                table: "Palestras",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Palestras",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
