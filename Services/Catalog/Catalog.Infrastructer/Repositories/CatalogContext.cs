public class CatalogContext : ICatalogContext
{
    public IProductRepository Products { get; }
    public IBrandRepository Brands { get; }
    public ITypesRepository Types { get; }

    public CatalogContext(
        IProductRepository productRepository,
        IBrandRepository brandsRepository,
        ITypesRepository typesRepository)
    {
        Products = productRepository;
        Brands = brandsRepository;
        Types = typesRepository;
    }

}