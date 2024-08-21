using MediatorResultPattern.Contract;

namespace Discount.Application.Errors;

public record InvalidRequestError(string Message) : InternalError(DiscountErrors.InvalidRequestError, Message) { }
