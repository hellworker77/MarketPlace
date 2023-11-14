using Domain.Entities;
using Domain.Identities;

namespace Persistence.SeedData;

public static class Data
{
    public static List<Product> Products => new ();
    public static List<Favorite> Favorites => new ();
    public static List<Media> Medias => new ();
    public static List<User> Users => new ();
}