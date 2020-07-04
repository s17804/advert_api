using System.Linq;
using AdvertApi.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AdvertApi.Models
{
    public class AdvertDbContext : DbContext 
    {
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Client> Clients { get; set; }
        
        protected AdvertDbContext()
        {
        }

        public AdvertDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BannerConfiguration());
            modelBuilder.ApplyConfiguration(new BuildingConfiguration());
            modelBuilder.ApplyConfiguration(new CampaignConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
        }
    }
}