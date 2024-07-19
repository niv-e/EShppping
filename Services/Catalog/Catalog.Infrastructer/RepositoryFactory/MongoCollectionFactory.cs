using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class MongoCollectionFactory
{
    private MongoClient? _mongoClient;
    private IMongoDatabase? _mongoDb;

    public MongoCollectionFactory(IConfiguration configurations)
    {
        _mongoClient = new MongoClient(configurations.GetValue<string>("DatabaseSettings:ConnectionString"));
        _mongoDb = _mongoClient.GetDatabase(configurations.GetValue<string>("DatabaseSettings:DatabaseName"));
    }
    public IMongoCollection<T> GetCollection<T>() where T : BaseEntity
    {
        return _mongoDb!.GetCollection<T>(typeof(T).Name);
    }
}
