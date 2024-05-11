public interface ITypesRepository
{
    Task<IEnumerable<ProductType>> GetProductTypes();
}
