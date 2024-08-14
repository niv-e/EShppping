using MongoDB.Driver;


namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> collection;

    public BrandRepository(MongoCollectionFactory collectionFactory )
    {
        collection = collectionFactory.GetCollection<ProductBrand>();
    }
    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await collection.Find(brand => true)
            .ToListAsync();     
    }
}
