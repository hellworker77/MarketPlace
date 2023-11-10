using Domain.Common;
using Domain.Entities;

namespace Application.Features.Products.Commands.CreateProduct;

public class ProductCreatedEvent : BaseEvent
{
    public ProductCreatedEvent(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}