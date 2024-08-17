using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Entities;

public record Coupon(string Id, string ProductName, string Description, int Amount)
{
}
