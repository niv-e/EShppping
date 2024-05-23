using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class MongoRepositoryFactory
{
    private static MongoClient? _mongoClient;
    private static IMongoDatabase? _mongoDb;

    public MongoRepositoryFactory(IConfiguration configurations)
    {
        _mongoClient = new MongoClient(configurations.GetValue<string>("DatabaseSettings:ConnectionString"));
        _mongoDb = _mongoClient.GetDatabase(configurations.GetValue<string>("DatabaseSettings:DatabaseName"));
    }
    public static IMongoCollection<T> CreateRepository<T>() where T : BaseEntity
    {
        return _mongoDb!.GetCollection<T>(typeof(T).Name);

    }
}
