using MediatorResultPattern.Enumerations;

namespace Discount.Application.Errors;

public class DiscountErrors : ErrorReason
{
    public static DiscountErrors Unknown = new(1, "Unknown");
    public static DiscountErrors InvalidRequestError = new(2, "InvalidRequestError");
    public DiscountErrors(int id, string name)
    : base(id, name)
    {
    }
}
