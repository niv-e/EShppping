using System.Text.Json.Nodes;

public abstract record OperationResult
{
    public bool Success { get; init; }
}
public record DeleteResult : OperationResult
{
    public long DeletedCount { get; init; }
}

public record GetOneResult<T> : OperationResult where T : BaseEntity
{
    public T? Entity { get; init; }
}

public record class UpdateOneResult : OperationResult
{
    public long ModifiedCount { get; init; }
    public long MatchedCount { get; init; }
    public bool IsAcknowledged { get; init; }
}