using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(favorite => favorite.Id);

        builder
            .HasOne(favorite => favorite.User)
            .WithMany(user => user.Favorites)
            .OnDelete(DeleteBehavior.Cascade);
    }
}