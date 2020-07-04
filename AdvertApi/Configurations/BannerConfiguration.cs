using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApi.Configurations
{
    public class BannerConfiguration : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.HasKey(banner => banner.IdAdvertisement);

            builder.Property(banner => banner.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(banner => banner.Price)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.Property(banner => banner.Area)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.HasOne(banner => banner.Campaign)
                .WithMany(campaign => campaign.Banners)
                .HasForeignKey("IdCampaign");
            
        }
    }
}