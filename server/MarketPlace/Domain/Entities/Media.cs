using Domain.Common;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Domain.Entities;

public class Media : BaseAuditableEntity
{
    public string FileTitle { get; set; }
    public byte[] Data { get; set; }
    
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}