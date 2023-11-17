using Domain.Common;
using Domain.Identities;

#pragma warning disable CS8618

namespace Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Title { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public byte Rate { get; set; }
    public int RemainingCount { get; set; }
    
    public virtual List<Media> Medias { get; set; }
    
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}