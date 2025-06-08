using EnergyControlAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace EnergyControlAPI.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        // PROPRIEDADE PARA MANIPULAR A ENTIDADE DE USER
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity => {
                entity.ToTable("users");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Name).IsRequired();
                entity.Property(entity => entity.Email).IsRequired();
                entity.Property(entity=> entity.Role).IsRequired();
                entity.HasIndex(entity => entity.Email).IsUnique();
            });
        }
        public DatabaseContext(DbContextOptions options): base(options) { }

        protected DatabaseContext() { }
    }
}
