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
        public DbSet<Foto> Fotos { get; set; }

        public DbSet<AdmHotel> AdmHotel { get; set; } = default!;

        public DbSet<Comentario> Comentario { get; set; } = default!;

        public DbSet<Hospede> Hospede { get; set; } = default!;

        public DbSet<Hotel> Hotel { get; set; } = default!;

        public DbSet<Quarto> Quarto { get; set; } = default!;

        public DbSet<Reserva> Reserva { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Quarto)
                .WithMany(q => q.Reservas)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
