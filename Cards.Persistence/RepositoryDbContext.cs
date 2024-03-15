using Cards.Domain.Entities;
using Cards.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Cards.Persistence
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());

            //modelBuilder.Entity<Card>.
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CardStatus> Status { get; set; }
    }
}
