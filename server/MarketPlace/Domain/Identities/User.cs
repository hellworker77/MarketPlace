using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identities;

public class User : IdentityUser<Guid>
{
    public virtual List<Product> Products { get; set; }
    public virtual List<Favorite> Favorites { get; set; }
}