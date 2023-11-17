using Application.Interfaces;
using Persistence.Contexts;

namespace Persistence.SeedData;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _applicationDbContext;

    public DbInitializer(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Initialize()
    {
        _applicationDbContext.Database.EnsureDeleted();
        _applicationDbContext.Database.EnsureCreated();

        _applicationDbContext.Roles.AddRange(Data.Roles);
        _applicationDbContext.SaveChanges();

        _applicationDbContext.Users.AddRange(Data.Users);
        _applicationDbContext.SaveChanges();

        _applicationDbContext.UserRoles.AddRange(Data.UserRoles);
        _applicationDbContext.SaveChanges();

        _applicationDbContext.Products.AddRange(Data.Products);
        _applicationDbContext.SaveChanges();

        _applicationDbContext.Favorites.AddRange(Data.Favorites);
        _applicationDbContext.SaveChanges();

        _applicationDbContext.Medias.AddRange(Data.Medias);
        _applicationDbContext.SaveChanges();
    }
}