using CSharpFunctionalExtensions;
using MediatR;

namespace MediatorResultPattern.Contract;

public interface IResultRequestHandler<TInput, TOutput> : IRequestHandler<TInput, Result<TOutput, InternalError>> where TInput : IResultRequest<TOutput>;
