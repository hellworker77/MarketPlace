using Domain.Common;
using Domain.Entities;

namespace Application.Features.Products.Commands.UpdateProduct;

public class ProductUpdatedEvent : BaseEvent
{
    public ProductUpdatedEvent(Product product)
    {
        Product = product;
    }
    
    public Product Product { get; }
}