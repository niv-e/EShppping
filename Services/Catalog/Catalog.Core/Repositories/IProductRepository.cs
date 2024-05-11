public interface IProductRepository
{
    Task<IAsyncEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(int id);
    Task<IAsyncEnumerable<Product>> GetProductsByName(string name);
    Task<IAsyncEnumerable<Product?>> GetProductsByBrand(string brand);
    Task<OperationResult<Product>> CreateProduct(Product product);
    Task<OperationResult<Product>> UpdateProduct(Product product);
    Task<OperationResult<Product>> DeleteProduct(int id);
}
