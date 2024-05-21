using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public interface IRepository<T> where T : BaseEntity
{

}

public class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _documentsCollection;
    public MongoRepository(IMongoCollection<T> documentsCollection)
    {
        _documentsCollection = documentsCollection;
    }
}

public class RepositoryFactory
{
    private static MongoClient? _mongoClient;
    private static IMongoDatabase? _mongoDb;

    public RepositoryFactory(IConfiguration configurations)
    {
        _mongoClient = new MongoClient(configurations.GetValue<string>("DatabaseSettings:ConnectionString"));
        _mongoDb = _mongoClient.GetDatabase(configurations.GetValue<string>("DatabaseSettings:DatabaseName"));
    }
    public static IRepository<T> CreateRepository<T>() where T : BaseEntity
    {
        return new MongoRepository<T>(_mongoDb!.GetCollection<T>(typeof(T).Name));
    }
}

public interface ICatalogContext
{
    public IRepository<Product> Products { get; }
    public IRepository<ProductBrand> Brands { get; }
    public IRepository<ProductType> Types { get; }

}

public class CatalogContext: ICatalogContext
{
    public IRepository<Product> Products { get; }
    public IRepository<ProductBrand> Brands { get; }
    public IRepository<ProductType> Types { get; }

    public CatalogContext(
        IRepository<Product> productRepository, 
        IRepository<ProductBrand>brandsRepository,
        IRepository<ProductType> typesRepository)
    {
        Products = productRepository;
        Brands = brandsRepository;
        Types = typesRepository;
    }

}