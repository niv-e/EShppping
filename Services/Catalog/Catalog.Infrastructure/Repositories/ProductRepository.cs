using Catalog.Core.Specs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(MongoCollectionFactory collectionFactory)
    {
        _collection = collectionFactory.GetCollection<Product>();
    }
    public async Task<Product> CreateProduct(Product product)
    {
        await _collection.InsertOneAsync(product);
        return product;
    }
    public async Task<DeleteResult> DeleteProduct(string id)
    {
        var deleteResults = await _collection.DeleteOneAsync(filter: product => product.Id! == id);
        return new DeleteResult
        { 
            Success = deleteResults.IsAcknowledged,
            DeletedCount = deleteResults.DeletedCount
        };
    }

    public async Task<Product?> GetProductById(string id)
    { 
        return await _collection.Find(product => product.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams)
    {
        var filter = BuildFilter(catalogSpecParams);
        var products = await GetFilteredProducts(catalogSpecParams, filter);
        var count = await GetProductsCount();

        return new Pagination<Product>(catalogSpecParams.PageIndex, catalogSpecParams.PageSize, count, products);
    }

    public async Task<UpdateOneResult> UpdateProduct(Product product)
    {
        var replceResult = await _collection.ReplaceOneAsync(p => p.Id == product.Id,product);
        return new UpdateOneResult
        {
            ModifiedCount = replceResult.ModifiedCount,
            MatchedCount = replceResult.MatchedCount,
            IsAcknowledged = replceResult.IsAcknowledged
        };
    }

    private FilterDefinition<Product> BuildFilter(CatalogSpecParams catalogSpecParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        filter = ApplySearchFilter(filter, builder, catalogSpecParams.Search);
        filter = ApplyBrandFilter(filter, builder, catalogSpecParams.BrandId);
        filter = ApplyTypeFilter(filter, builder, catalogSpecParams.TypeId);

        return filter;
    }
    private FilterDefinition<Product> ApplySearchFilter(FilterDefinition<Product> filter, FilterDefinitionBuilder<Product> builder, string? search)
    {
        return !string.IsNullOrEmpty(search) ? filter & builder.Regex(x => x.Name, new BsonRegularExpression(search)) : filter;
    }
    private FilterDefinition<Product> ApplyBrandFilter(FilterDefinition<Product> filter, FilterDefinitionBuilder<Product> builder, string? brandId)
    {
        return !string.IsNullOrEmpty(brandId) ? filter & builder.Eq(x => x.Brand!.Id, brandId) : filter;
    }

    private FilterDefinition<Product> ApplyTypeFilter(FilterDefinition<Product> filter, FilterDefinitionBuilder<Product> builder, string? typeId)
    {
        return !string.IsNullOrEmpty(typeId) ? filter & builder.Eq(x => x.Type!.Id, typeId) : filter;
    }
    private SortDefinition<Product> BuildSort(string? sort)
    {
        return !string.IsNullOrEmpty(sort) ? Builders<Product>.Sort.Ascending(sort) : Builders<Product>.Sort.Ascending(x => x.Name);
    }

    private async Task<IReadOnlyList<Product>> GetFilteredProducts(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        return await DataFilter(catalogSpecParams, filter);
    }
    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var sort = catalogSpecParams.Sort switch
        {
            "priceAsc" => Builders<Product>.Sort.Ascending("Price"),
            "priceDesc" => Builders<Product>.Sort.Descending("Price"),
            _ => Builders<Product>.Sort.Ascending("Name")
        };

        return await _collection
            .Find(filter)
            .Sort(sort)
            .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
            .Limit(catalogSpecParams.PageSize)
            .ToListAsync();
    }
    private async Task<IReadOnlyList<Product>> GetFilteredProducts(FilterDefinition<Product> filter, SortDefinition<Product> sort, int pageIndex, int pageSize)
    {
        return await _collection
            .Find(filter)
            .Sort(sort)
            .Skip(pageSize * (pageIndex - 1))
            .Limit(pageSize)
            .ToListAsync();
    }
    private async Task<long> GetProductsCount()
    {
        return await _collection.CountDocumentsAsync(p => true);
    }

}

