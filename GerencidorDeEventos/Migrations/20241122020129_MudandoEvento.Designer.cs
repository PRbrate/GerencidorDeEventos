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
    [Migration("20241122020129_MudandoEvento")]
    partial class MudandoEvento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventoUsuario", b =>
                {
                    b.Property<int>("EventoId")
                        .HasColumnType("integer");

                    b.Property<int>("UsuariosInscritosId")
                        .HasColumnType("integer");

                    b.HasKey("EventoId", "UsuariosInscritosId");

                    b.HasIndex("UsuariosInscritosId");

                    b.ToTable("Inscricoes", (string)null);
                });

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

                    b.Property<DateTime>("HoraFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("interval");

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

            modelBuilder.Entity("EventoUsuario", b =>
                {
                    b.HasOne("GerencidorDeEventos.Model.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerencidorDeEventos.Model.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosInscritosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
