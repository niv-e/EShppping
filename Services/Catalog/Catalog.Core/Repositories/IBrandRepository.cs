public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrands();
}

