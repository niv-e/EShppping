using MongoDB.Driver;
using System.Text.Json;

internal class GenericJsonDataSeeder
{
    internal static async Task SeedData<T>(IMongoCollection<T> mongoCollection, string path) where T : BaseEntity
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