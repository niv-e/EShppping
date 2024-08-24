using LanguageExt.Common;
using MediatR;

namespace MediatorResultPattern.Contract;

public interface IResultRequestHandler<TInput, TOutput> : IRequestHandler<TInput, Result<TOutput>> where TInput : IResultRequest<TOutput>;
