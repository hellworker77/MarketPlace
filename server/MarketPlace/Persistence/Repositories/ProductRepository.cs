using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IGenericRepository<Product> _repository;

    public ProductRepository(IGenericRepository<Product> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Product>> GetProductsByTitleAsync(string title)
    {
        return await _repository.Entities
            .Where(product => product.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();
    }
}