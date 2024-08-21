using CSharpFunctionalExtensions;
using MediatR;

namespace MediatorResultPattern.Contract;

//https://goatreview.com/improving-error-handling-result-pattern-mediatr/
public interface IResultRequest<T> : IRequest<Result<T, InternalError>>;
