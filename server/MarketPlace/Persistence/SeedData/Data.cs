using Domain.Entities;
using Domain.Identities;
using Microsoft.AspNetCore.Identity;

namespace Persistence.SeedData;

public static class Data
{
    public static readonly List<IdentityRole<Guid>> Roles = new()
    {
        new IdentityRole<Guid>
        {
            Id = Guid.NewGuid(),
            Name = "admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString("D")
        }
    };
    public static readonly List<User> Users = new()
    {
        new User()
        {
            Id = Guid.NewGuid(),
            Email = "admin@aaa.com",
            NormalizedEmail = "ADMIN@AAA.COM",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAIAAYagAAAAEOObrxK8MEi9CAr6V1lm3CjQwpdMWO46J15/fN4AshwLh45ThOxSLoOFh1id4JNFQA==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D")
        }
    };
    public static readonly List<IdentityUserRole<Guid>> UserRoles = new ()
    {
        new IdentityUserRole<Guid>
        {
            UserId = Users.FirstOrDefault()!.Id,
            RoleId = Roles.FirstOrDefault()!.Id
        }
    };
    public static readonly List<Product> Products = new()
    {
        new Product()
        {
            Title = "product title",
            Price = 12.24f,
            Description = "product description",
            Category = "product Category",
            Rate = 4,
            RemainingCount = 2,
            UserId = Users.FirstOrDefault()!.Id
        }
    };
    public static List<Favorite> Favorites => new ();
    public static List<Media> Medias => new ();
   
}