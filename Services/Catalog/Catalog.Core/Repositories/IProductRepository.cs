﻿public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize);
    Task<Product?> GetProductById(string id);
    Task<IEnumerable<Product>> GetProductsBy(ProductsFilter productFilter);
    Task<Product> CreateProduct(Product product);
    Task<UpdateOneResult> UpdateProduct(Product product);
    Task<DeleteResult> DeleteProduct(string id);
}
