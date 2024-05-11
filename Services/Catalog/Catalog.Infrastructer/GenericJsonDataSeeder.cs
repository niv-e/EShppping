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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        List<T> entities = JsonSerializer.Deserialize<List<T>>(jsonData);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        if (entities is not null )
        {
            await mongoCollection.InsertManyAsync(entities);
        }
    }
}