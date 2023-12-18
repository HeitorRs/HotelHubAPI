using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelHub.Models;
using static HotelHub.Models.Comentario;

namespace HotelHub.Data
{
    public class HotelHubContext : DbContext {
        public HotelHubContext(DbContextOptions<HotelHubContext> options)
            : base(options) {
        }

        public DbSet<HotelHub.Models.AdmHotel> AdmHotel { get; set; } = default!;

        public DbSet<HotelHub.Models.Comentario> Comentario { get; set; } = default!;

        public DbSet<HotelHub.Models.FotoHotel> FotoHotel { get; set; } = default!;

        public DbSet<HotelHub.Models.FotoQuarto> FotoQuarto { get; set; } = default!;

        public DbSet<HotelHub.Models.Hospede> Hospede { get; set; } = default!;

        public DbSet<HotelHub.Models.Hotel> Hotel { get; set; } = default!;

        public DbSet<HotelHub.Models.Quarto> Quarto { get; set; } = default!;

        public DbSet<HotelHub.Models.Reserva> Reserva { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Admhotel relation
            modelBuilder.Entity<AdmHotel>()
                .HasKey(a => a.AdmHotelId);

            modelBuilder.Entity<AdmHotel>()
                .Property(a => a.AdmHotelId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AdmHotel>()
                .HasMany(a => a.HoteisGerenciados)
                .WithOne(h => h.Administrador);

            //Comentario relation
            modelBuilder.Entity<Comentario>()
                .HasKey(c => c.ComentarioId);

            modelBuilder.Entity<Comentario>()
                .Property(c => c.ComentarioId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Hospede)
                .WithMany();

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Quarto)
                .WithMany();

            //FotoHotel relation
            modelBuilder.Entity<FotoHotel>()
                .HasKey(f => f.FotoId);

            modelBuilder.Entity<FotoHotel>()
                .Property(f => f.FotoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FotoHotel>()
                .Property(f => f.NomeArquivo)
                .IsRequired();

            modelBuilder.Entity<FotoHotel>()
                .HasOne(f => f.Hotel)
                .WithMany(h => h.FotosHotel);

            //FotoQuarto relation
            modelBuilder.Entity<FotoQuarto>()
                .HasKey(f => f.FotoId);

            modelBuilder.Entity<FotoQuarto>()
                .Property(f => f.FotoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FotoQuarto>()
                .Property(f => f.NomeArquivo)
                .IsRequired();

            modelBuilder.Entity<FotoQuarto>()
                .HasOne(f => f.Quarto)
                .WithMany(q => q.FotosQuarto);

            //Hospede relation
            modelBuilder.Entity<Hospede>()
                .HasKey(h => h.HospedeId);

            modelBuilder.Entity<Hospede>()
                .Property(h => h.HospedeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Hospede>()
                .Property(h => h.Nome)
                .IsRequired();

            modelBuilder.Entity<Hospede>()
                .Property(h => h.Sobrenome)
                .IsRequired();

            modelBuilder.Entity<Hospede>()
                .Property(h => h.Email)
                .IsRequired();

            modelBuilder.Entity<Hospede>()
                .Property(h => h.Senha)
                .IsRequired();

            modelBuilder.Entity<Hospede>()
                .HasMany(h => h.Reservas)
                .WithOne(r => r.Hospede);

            modelBuilder.Entity<Hospede>()
                .HasMany(h => h.Comentarios)
                .WithOne(c => c.Hospede);

            //Hotel relation
            modelBuilder.Entity<Hotel>()
                .HasKey(h => h.HotelId);

            modelBuilder.Entity<Hotel>()
                .Property(h => h.HotelId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Nome)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Descricao)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Cidade)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Quartos)
                .WithOne(q => q.Hotel);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Reservas)
                .WithOne(r => r.Hotel);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Administrador)
                .WithMany(a => a.HoteisGerenciados);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.FotosHotel)
                .WithOne(f => f.Hotel);

            //Quarto relation
            modelBuilder.Entity<Quarto>()
                .HasKey(q => q.QuartoId);

            modelBuilder.Entity<Quarto>()
                .Property(q => q.QuartoId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Quarto>()
                .Property(q => q.Descricao)
                .IsRequired();

            modelBuilder.Entity<Quarto>()
                .Property(q => q.Preco)
                .IsRequired();

            modelBuilder.Entity<Quarto>()
                .HasMany(q => q.Reservas)
                .WithOne(r => r.Quarto);

            modelBuilder.Entity<Quarto>()
                .HasOne(q => q.Hotel)
                .WithMany(h => h.Quartos);

            modelBuilder.Entity<Quarto>()
                .HasMany(q => q.FotosQuarto)
                .WithOne(f => f.Quarto);

            modelBuilder.Entity<Quarto>()
                .HasMany(q => q.ComentariosQuarto)
                .WithOne(c => c.Quarto);

            //Reserva relation
            modelBuilder.Entity<Reserva>()
                .HasKey(r => r.ReservaId);

            modelBuilder.Entity<Reserva>()
                .Property(r => r.ReservaId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Reserva>()
                .Property(r => r.DataEntrada)
                .IsRequired();

            modelBuilder.Entity<Reserva>()
                .Property(r => r.DataSaida)
                .IsRequired();

            modelBuilder.Entity<Reserva>()
                .Property(r => r.Observacao);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Hospede)
                .WithMany(h => h.Reservas);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Quarto)
                .WithMany(q => q.Reservas)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reservas)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
