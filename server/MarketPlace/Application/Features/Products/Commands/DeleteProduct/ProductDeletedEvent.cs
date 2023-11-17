using Domain.Common;
using Domain.Entities;

namespace Application.Features.Products.Commands.DeleteProduct;

public class ProductDeletedEvent : BaseEvent
{
    public ProductDeletedEvent(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}