using Application.Common.Mappings;
using Domain.Entities;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Application.Features.Products.Queries.GetProductByTitle;

public class GetProductByTitleDto : IMapFrom<Product>
{
    public string Title { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public byte Rate { get; set; }
    public int RemainingCount { get; set; }
}