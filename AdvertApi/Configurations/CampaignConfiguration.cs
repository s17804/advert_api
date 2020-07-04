using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApi.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(campaign => campaign.IdCampaign);

            builder.Property(campaign => campaign.StartDate)
                .IsRequired();

            builder.Property(campaign => campaign.EndDate)
                .IsRequired();

            builder.Property(campaign => campaign.PricePerSquareMeter)
                .HasPrecision(6, 2)
                .IsRequired();
            
            builder.HasOne(campaign => campaign.Client)
                .WithMany(client => client.Campaigns)
                .HasForeignKey("IdClient");

            builder.HasOne(campaign => campaign.BuildingFrom)
                .WithMany(building => building.CampaignsFrom)
                .HasForeignKey("FromIdBuilding");

            builder.HasOne(campaign => campaign.BuildingTo)
                .WithMany(building => building.CampaignsTo)
                .HasForeignKey("ToIdBuilding");

        }
    }
}