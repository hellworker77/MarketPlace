using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetProductsByTitleAsync(string title);
    Task<Product?> GetByIdWithUserIdAsync(Guid id, Guid userId);
}