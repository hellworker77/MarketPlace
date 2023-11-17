using Application.Common.Mappings;
using Domain.Entities;

#pragma warning disable CS8618
namespace Application.Features.Products.Queries.GetAllProducts;

public class GelAllProductsDto : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public float Price { get; set; }
    public string Category { get; set; }
    public byte Rate { get; set; }
}