using MongoDB.Driver;


namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> collection;

    public BrandRepository(IMongoCollection<ProductBrand> mongoRepository)
    {
        collection = mongoRepository;
    }
    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await collection.Find(brand => true)
            .ToListAsync();     
    }
}
