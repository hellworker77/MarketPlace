using Persistence.Contexts;

namespace Persistence.SeedData;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _dbContext;

    public DbInitializer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Initialize()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        
    }
}