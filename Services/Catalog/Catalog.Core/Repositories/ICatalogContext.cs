public interface ICatalogContext
{
    public IProductRepository Products { get; }
    public IBrandRepository Brands { get; }
    public ITypesRepository Types { get; }
}
