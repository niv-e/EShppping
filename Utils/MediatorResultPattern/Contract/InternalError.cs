using MediatorResultPattern.Enumerations;

namespace MediatorResultPattern.Contract;

//https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
public abstract record InternalError(ErrorReason Reason, string Message);
