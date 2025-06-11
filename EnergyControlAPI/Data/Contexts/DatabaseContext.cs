using EnergyControlAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyControlAPI.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        // PROPRIEDADE PARA MANIPULAR A ENTIDADE DE USER
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<EquipmentModel> Equipments { get;  set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração UserModel
            modelBuilder.Entity<UserModel>(entity => {
                entity.ToTable("users");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Name).IsRequired();
                entity.Property(entity => entity.Email).IsRequired();
                entity.Property(entity=> entity.Role).IsRequired();
                entity.HasIndex(entity => entity.Email).IsUnique();
            });

            // Configuração Equipment
            modelBuilder.Entity<EquipmentModel>(entity => {
                entity.ToTable("equipment");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Type).IsRequired();

                entity.HasOne(e => e.Sector)
                      .WithMany(s => s.Equipments)
                      .HasForeignKey(e => e.SectorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
        public DatabaseContext(DbContextOptions options): base(options) { }

        protected DatabaseContext() { }
    }
}
