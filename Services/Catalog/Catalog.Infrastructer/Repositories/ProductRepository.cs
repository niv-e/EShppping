﻿using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> collection;

    public ProductRepository(MongoCollectionFactory collectionFactory)
    {
        collection = collectionFactory.GetCollection<Product>();

    }
    public async Task<Product> CreateProduct(Product product)
    {
        await collection.InsertOneAsync(product);
        return product;
    }
    public async Task<DeleteResult> DeleteProduct(string id)
    {
        var deleteResults = await collection.DeleteOneAsync(filter: product => product.Id! == id);
        return new DeleteResult
        { 
            Success = deleteResults.IsAcknowledged,
            DeletedCount = deleteResults.DeletedCount
        };
    }

    public async Task<Product?> GetProductById(string id)
    { 
        return await collection.Find(product => product.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts(int pageNumber = 1, int pageSize = int.MaxValue)
    {
        if(pageSize == int.MaxValue)
        {
            return await collection.Find( _ => true)
                .ToListAsync();
        }

        var skip = (pageNumber - 1) * pageSize;
        return await collection.Find(FilterDefinition<Product>.Empty)
                                     .Skip(skip)
                                     .Limit(pageSize)
                                     .ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsBy(ProductsFilter productsFilter)
    {
        var filter = Builders<Product>.Filter.Eq(product => product!.Name, productsFilter.ProductName);
        filter = filter | Builders<Product>.Filter.Eq(product => product!.Brand!.Name, productsFilter.ProductBrand);

        return await collection.Find(filter)
            .ToListAsync();
    }

    public async Task<UpdateOneResult> UpdateProduct(Product product)
    {
        var replceResult = await collection.ReplaceOneAsync(p => p.Id == product.Id,product);
        return new UpdateOneResult
        {
            ModifiedCount = replceResult.ModifiedCount,
            MatchedCount = replceResult.MatchedCount,
            IsAcknowledged = replceResult.IsAcknowledged
        };
    }

}

