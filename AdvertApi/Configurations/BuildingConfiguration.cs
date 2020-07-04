using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApi.Configurations
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(building => building.IdBuilding);

            builder.Property(building => building.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(building => building.Street)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(building => building.StreetNumber)
                .IsRequired();

            builder.Property(building => building.Height)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.HasMany(building => building.CampaignsFrom)
                .WithOne(campaign => campaign.BuildingFrom);

            builder.HasMany(building => building.CampaignsTo)
                .WithOne(campaign => campaign.BuildingTo);
            
        }
    }
}