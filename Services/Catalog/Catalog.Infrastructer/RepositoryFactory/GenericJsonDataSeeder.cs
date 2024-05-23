using MongoDB.Driver;
using System.Text.Json;

public class GenericJsonDataSeeder<T> where T: BaseEntity
{
    public static async Task SeedData(IMongoCollection<T> mongoCollection, string path)
    {
        var documentCountEsimation =
            await mongoCollection.EstimatedDocumentCountAsync(new EstimatedDocumentCountOptions());
           
        if (documentCountEsimation > 0)
        {
            return;
        }

        var jsonData = await File.ReadAllTextAsync(path);
        List<T> entities = JsonSerializer.Deserialize<List<T>>(jsonData) ?? throw new NullReferenceException();

        await mongoCollection.InsertManyAsync(entities);
    }
}