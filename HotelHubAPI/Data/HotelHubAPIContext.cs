using Microsoft.EntityFrameworkCore;
using HotelHubAPI.Models;

namespace HotelHubAPI.Data
{
    public class HotelHubAPIContext : DbContext {
        public HotelHubAPIContext(DbContextOptions<HotelHubAPIContext> options)
            : base(options) {
        }

        public DbSet<HotelHubAPI.Models.Hospede> Hospede { get; set; } = default!;

        public DbSet<HotelHubAPI.Models.AdmHotel> AdmHotel { get; set; } = default!;

        public DbSet<HotelHubAPI.Models.Comentario> Comentario { get; set; } = default!;

        public DbSet<HotelHubAPI.Models.Hotel> Hotel { get; set; } = default!;

        public DbSet<HotelHubAPI.Models.Quarto> Quarto { get; set; } = default!;

        public DbSet<HotelHubAPI.Models.Reserva> Reserva { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Configurações de relacionamentos, chaves estrangeiras, etc.
            modelBuilder.Entity<Hospede>()
                .HasMany(h => h.Reservas)
                .WithOne(r => r.Hospede)
                .HasForeignKey(r => r.HospedeId);

            modelBuilder.Entity<Hospede>()
                .HasMany(h => h.Comentarios)
                .WithOne(c => c.Hospede)
                .HasForeignKey(c => c.HospedeId);

            modelBuilder.Entity<AdmHotel>()
                .HasMany(a => a.Hoteis)
                .WithOne(h => h.AdmHotel)
                .HasForeignKey(h => h.AdmHotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Quartos)
                .WithOne(q => q.Hotel)
                .HasForeignKey(q => q.HotelId);

            modelBuilder.Entity<Quarto>().HasOne(q => q.Reserva).WithOne(r => r.Quarto).HasForeignKey<Reserva>(r => r.QuartoId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Quarto>()
                .HasMany(q => q.Comentarios)
                .WithOne(c => c.Quarto)
                .HasForeignKey(c => c.QuartoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
