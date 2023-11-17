using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(media => media.Id);

        builder
            .HasOne(media => media.Product)
            .WithMany(product => product.Medias)
            .OnDelete(DeleteBehavior.Cascade);
    }
}