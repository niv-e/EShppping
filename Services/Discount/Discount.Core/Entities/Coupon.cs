using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Entities;

public record Coupon
{
    public string? Id { get; set; }
    public string? ProductName { get; set;}
    public string? Description { get; set;}
    public int Amount { get; set; }
    public bool IsActive { get; set; }
}
