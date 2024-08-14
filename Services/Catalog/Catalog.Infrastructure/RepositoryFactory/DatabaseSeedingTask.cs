using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.RepositoryFactory;

public class DatabaseSeedingTask
{
    private readonly MongoCollectionFactory _mongoCollectionFactory;

    public DatabaseSeedingTask(MongoCollectionFactory mongoCollectionFactory)
    {
        _mongoCollectionFactory = mongoCollectionFactory;
    }

    public async Task Run()
    {
        var productsSeedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "products.json");
        var brandsSeedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "brands.json");
        var typesSeedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SeedData", "types.json");

        await GenericJsonDataSeeder.SeedData(_mongoCollectionFactory.GetCollection<Product>(), productsSeedPath);
        await GenericJsonDataSeeder.SeedData(_mongoCollectionFactory.GetCollection<ProductBrand>(), brandsSeedPath);
        await GenericJsonDataSeeder.SeedData(_mongoCollectionFactory.GetCollection<ProductType>(), typesSeedPath);
    }
}
