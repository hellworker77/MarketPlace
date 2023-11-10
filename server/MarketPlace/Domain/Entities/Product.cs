using Domain.Common;

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
}