public record Product : BaseEntity
{
    public required string Name { get; init; }
    public required decimal Price{ get; init; }

    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    public ProductBrand? Brand { get; set; }
    public ProductType? Type { get; set; }
}
