public record OperationResult<T> where T : BaseEntity
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Entity { get; init; }
}
