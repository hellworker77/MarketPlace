using Domain.Common;
using Domain.Identities;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Domain.Entities;

public class Favorite : BaseAuditableEntity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}