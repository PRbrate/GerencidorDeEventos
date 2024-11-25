﻿// <auto-generated />
using System;
using GerencidorDeEventos.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GerencidorDeEventos.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20241124151734_relacionamentosMinicurso")]
    partial class relacionamentosMinicurso
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GerencidorDeEventos.Model.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CpfResponsavel")
                        .IsRequired()
                        .HasMaxLength(14)
                        .IsUnicode(true)
                        .HasColumnType("character varying(14)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataLimiteInscricao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NomeResponsavel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("NumVagas")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.InscricaoEvento", b =>
                {
                    b.Property<int>("EventoId")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("EventoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("InscricoesEvento");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.InscricaoMinicurso", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<int>("MinicursoId")
                        .HasColumnType("integer");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("UsuarioId", "MinicursoId");

                    b.HasIndex("MinicursoId");

                    b.ToTable("InscricoesMinicurso");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.InscricaoPalestra", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<int>("PalestraId")
                        .HasColumnType("integer");

                    b.HasKey("UsuarioId", "PalestraId");

                    b.HasIndex("PalestraId");

                    b.ToTable("InscricoesPalestra");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Minicurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CurriculoPalestrante")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("EventoId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LimiteInscricao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Palestrante")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("QuantidadeDeVagas")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Minicursos");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Palestras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CurriculoPalestrante")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("EventoId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HoraFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("interval");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Palestrante")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Palestras");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Administrador")
                        .HasColumnType("boolean");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .IsUnicode(true)
                        .HasColumnType("character varying(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.InscricaoEvento", b =>
                {
                    b.HasOne("GerencidorDeEventos.Model.Evento", "Evento")
                        .WithMany("Inscricoes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerencidorDeEventos.Model.Usuario", "Usuario")
                        .WithMany("Inscricoes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.InscricaoMinicurso", b =>
                {
                    b.HasOne("GerencidorDeEventos.Model.Minicurso", "Minicurso")
                        .WithMany("Inscricoes")
                        .HasForeignKey("MinicursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerencidorDeEventos.Model.Usuario", "Usuario")
                        .WithMany("InscricoesMinicurso")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Minicurso");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.InscricaoPalestra", b =>
                {
                    b.HasOne("GerencidorDeEventos.Model.Palestras", "Palestra")
                        .WithMany("Inscricoes")
                        .HasForeignKey("PalestraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerencidorDeEventos.Model.Usuario", "Usuario")
                        .WithMany("InscricaoPalestras")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Palestra");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Minicurso", b =>
                {
                    b.HasOne("GerencidorDeEventos.Model.Evento", "Evento")
                        .WithMany("Minicursos")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Palestras", b =>
                {
                    b.HasOne("GerencidorDeEventos.Model.Evento", "Evento")
                        .WithMany("Palestras")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Evento", b =>
                {
                    b.Navigation("Inscricoes");

                    b.Navigation("Minicursos");

                    b.Navigation("Palestras");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Minicurso", b =>
                {
                    b.Navigation("Inscricoes");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Palestras", b =>
                {
                    b.Navigation("Inscricoes");
                });

            modelBuilder.Entity("GerencidorDeEventos.Model.Usuario", b =>
                {
                    b.Navigation("InscricaoPalestras");

                    b.Navigation("Inscricoes");

                    b.Navigation("InscricoesMinicurso");
                });
#pragma warning restore 612, 618
        }
    }
}
