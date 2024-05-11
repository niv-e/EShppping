public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetProductBrands();
}

