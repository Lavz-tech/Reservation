using API_Rdv.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Rdv.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Salle> Salles { get; set; }
        public DbSet<Creneau> Creneaux { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        // Seed des données initiales
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salle>().HasData(
                new Salle { Id = 1, Nom = "Salle A", Capacite = 10 },
                new Salle { Id = 2, Nom = "Salle B", Capacite = 20 }
            );

            // DateTimeKind.Utc requis par PostgreSQL
            modelBuilder.Entity<Creneau>().HasData(
                new Creneau { Id = 1, SalleId = 1, Debut = new DateTime(2026, 11, 7, 9, 0, 0, DateTimeKind.Utc), Fin = new DateTime(2026, 11, 7, 10, 0, 0, DateTimeKind.Utc) },
                new Creneau { Id = 2, SalleId = 1, Debut = new DateTime(2026, 11, 7, 10, 0, 0, DateTimeKind.Utc), Fin = new DateTime(2026, 11, 7, 11, 0, 0, DateTimeKind.Utc) },
                new Creneau { Id = 3, SalleId = 1, Debut = new DateTime(2026, 11, 7, 14, 0, 0, DateTimeKind.Utc), Fin = new DateTime(2026, 11, 7, 15, 0, 0, DateTimeKind.Utc) },
                new Creneau { Id = 4, SalleId = 2, Debut = new DateTime(2026, 11, 7, 9, 0, 0, DateTimeKind.Utc), Fin = new DateTime(2026, 11, 7, 10, 0, 0, DateTimeKind.Utc) },
                new Creneau { Id = 5, SalleId = 2, Debut = new DateTime(2026, 11, 7, 11, 0, 0, DateTimeKind.Utc), Fin = new DateTime(2026, 11, 7, 12, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Nom = "Alice", Email = "alice@test.com" },
                new User { Id = 2, Nom = "Bob", Email = "bob@test.com" }
            );
        }
    }
}
