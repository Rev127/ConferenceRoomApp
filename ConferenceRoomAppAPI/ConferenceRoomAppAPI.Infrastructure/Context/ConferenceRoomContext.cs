using Microsoft.EntityFrameworkCore;
using ConferenceRoomAppAPI.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ConferenceRoomAppAPI.Data.Context
{
    public class ConferenceRoomContext : IdentityDbContext<Users>
    {
        public ConferenceRoomContext(DbContextOptions<ConferenceRoomContext> options) : base(options)
        {
        }
        public DbSet<Halls> Halls { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<HallServices> HallServices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Halls>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<Halls>()
                .Property(h => h.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<Halls>()
                .HasMany(h => h.Services)
                .WithMany(s => s.Halls)
                .UsingEntity(j => j.ToTable("Hall_HallServices"));


            modelBuilder.Entity<Orders>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Hall)
                .WithMany(h => h.Orders)
                .HasForeignKey(o => o.HallId);

            modelBuilder.Entity<Orders>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<HallServices>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<HallServices>()
                .Property(s => s.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<HallServices>()
                .HasMany(s => s.Orders)
                .WithMany(o => o.ReservedServices)
                .UsingEntity(j => j.ToTable("OrderHallServices"));

            modelBuilder.Entity<HallServices>()
                .HasData(
                    new HallServices { Id = 1, Name = "Проєктор", Price = 500.00m },
                    new HallServices { Id = 2, Name = "Wi-Fi", Price = 300.00m },
                    new HallServices { Id = 3, Name = "Звук", Price = 700.00m }
                );

            modelBuilder.Entity<Halls>()
                .HasData(
                    new Halls { Id = 1, Name = "Зал А", Capacity = 50, PricePerHour = 2000.00m },
                    new Halls { Id = 2, Name = "Зал B", Capacity = 100, PricePerHour = 3500.00m },
                    new Halls { Id = 3, Name = "Зал C:", Capacity = 30, PricePerHour = 1500.00m }
                );

            base.OnModelCreating(modelBuilder);
        }



    }
}
