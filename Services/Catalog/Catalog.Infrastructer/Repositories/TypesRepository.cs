using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class TypesRepository : ITypesRepository
{
    private readonly IMongoCollection<ProductType> collection;

    public TypesRepository(IMongoCollection<ProductType> typessCollection)
    {
        collection = typessCollection;
    }

    public async Task<IEnumerable<ProductType>> GetProductTypes()
    {
        return await collection.Find(type => true)
            .ToListAsync();
    }
}
