using MediatorResultPattern.Contract;

namespace Discount.Application.Errors;

public record UnknownError(string Message) : InternalError(DiscountErrors.Unknown, Message) { }
