using Catalog.Core.Specs;

public interface IProductRepository
{
    Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams);
    Task<Product?> GetProductById(string id);
    Task<Product> CreateProduct(Product product);
    Task<UpdateOneResult> UpdateProduct(Product product);
    Task<DeleteResult> DeleteProduct(string id);
}
