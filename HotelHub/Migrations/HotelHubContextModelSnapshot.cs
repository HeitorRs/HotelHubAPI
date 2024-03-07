﻿// <auto-generated />
using System;
using HotelHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelHub.Migrations
{
    [DbContext(typeof(HotelHubContext))]
    partial class HotelHubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelHub.Models.AdmHotel", b =>
                {
                    b.Property<int>("AdmHotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdmHotelId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdmHotelId");

                    b.ToTable("AdmHotel");
                });

            modelBuilder.Entity("HotelHub.Models.Comentario", b =>
                {
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComentarioId"));

                    b.Property<int?>("HospedeId")
                        .HasColumnType("int");

                    b.Property<int?>("QuartoId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComentarioId");

                    b.HasIndex("HospedeId");

                    b.HasIndex("QuartoId");

                    b.ToTable("Comentario");
                });

            modelBuilder.Entity("HotelHub.Models.FotoHotel", b =>
                {
                    b.Property<int>("FotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FotoId"));

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("NomeArquivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FotoId");

                    b.HasIndex("HotelId");

                    b.ToTable("FotoHotel");
                });

            modelBuilder.Entity("HotelHub.Models.FotoQuarto", b =>
                {
                    b.Property<int>("FotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FotoId"));

                    b.Property<string>("NomeArquivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuartoId")
                        .HasColumnType("int");

                    b.HasKey("FotoId");

                    b.HasIndex("QuartoId");

                    b.ToTable("FotoQuarto");
                });

            modelBuilder.Entity("HotelHub.Models.Hospede", b =>
                {
                    b.Property<int>("HospedeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HospedeId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HospedeId");

                    b.ToTable("Hospede");
                });

            modelBuilder.Entity("HotelHub.Models.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<int>("AdministradorAdmHotelId")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HotelId");

                    b.HasIndex("AdministradorAdmHotelId");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("HotelHub.Models.Quarto", b =>
                {
                    b.Property<int>("QuartoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuartoId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<float>("Preco")
                        .HasColumnType("real");

                    b.HasKey("QuartoId");

                    b.HasIndex("HotelId");

                    b.ToTable("Quarto");
                });

            modelBuilder.Entity("HotelHub.Models.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservaId"));

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSaida")
                        .HasColumnType("datetime2");

                    b.Property<int>("HospedeId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuartoId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId");

                    b.HasIndex("HospedeId");

                    b.HasIndex("HotelId");

                    b.HasIndex("QuartoId");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("HotelHub.Models.Comentario", b =>
                {
                    b.HasOne("HotelHub.Models.Hospede", "Hospede")
                        .WithMany("Comentarios")
                        .HasForeignKey("HospedeId");

                    b.HasOne("HotelHub.Models.Quarto", "Quarto")
                        .WithMany("ComentariosQuarto")
                        .HasForeignKey("QuartoId");

                    b.Navigation("Hospede");

                    b.Navigation("Quarto");
                });

            modelBuilder.Entity("HotelHub.Models.FotoHotel", b =>
                {
                    b.HasOne("HotelHub.Models.Hotel", "Hotel")
                        .WithMany("FotosHotel")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelHub.Models.FotoQuarto", b =>
                {
                    b.HasOne("HotelHub.Models.Quarto", "Quarto")
                        .WithMany("FotosQuarto")
                        .HasForeignKey("QuartoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quarto");
                });

            modelBuilder.Entity("HotelHub.Models.Hotel", b =>
                {
                    b.HasOne("HotelHub.Models.AdmHotel", "Administrador")
                        .WithMany("HoteisGerenciados")
                        .HasForeignKey("AdministradorAdmHotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrador");
                });

            modelBuilder.Entity("HotelHub.Models.Quarto", b =>
                {
                    b.HasOne("HotelHub.Models.Hotel", "Hotel")
                        .WithMany("Quartos")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HotelHub.Models.Reserva", b =>
                {
                    b.HasOne("HotelHub.Models.Hospede", "Hospede")
                        .WithMany("Reservas")
                        .HasForeignKey("HospedeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelHub.Models.Hotel", "Hotel")
                        .WithMany("Reservas")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HotelHub.Models.Quarto", "Quarto")
                        .WithMany("Reservas")
                        .HasForeignKey("QuartoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hospede");

                    b.Navigation("Hotel");

                    b.Navigation("Quarto");
                });

            modelBuilder.Entity("HotelHub.Models.AdmHotel", b =>
                {
                    b.Navigation("HoteisGerenciados");
                });

            modelBuilder.Entity("HotelHub.Models.Hospede", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("HotelHub.Models.Hotel", b =>
                {
                    b.Navigation("FotosHotel");

                    b.Navigation("Quartos");

                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("HotelHub.Models.Quarto", b =>
                {
                    b.Navigation("ComentariosQuarto");

                    b.Navigation("FotosQuarto");

                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
