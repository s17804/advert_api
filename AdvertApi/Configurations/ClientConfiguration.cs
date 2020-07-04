using AdvertApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertApi.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(client => client.IdClient);

            builder.Property(client => client.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(client => client.LastName)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(client => client.Email)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(client => client.Phone)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(client => client.Login)
                .IsUnique();

            builder.Property(client => client.Login)
                .HasMaxLength(100);

            builder.Property(client => client.Password)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(client => client.RefreshToken)
                .HasMaxLength(32);
            
            builder.Property(client => client.Salt)
                .HasMaxLength(32)
                .IsRequired();

            builder.HasMany(client => client.Campaigns)
                .WithOne(campaign => campaign.Client);
        }
    }
}