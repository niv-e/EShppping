using Cart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Application.Components;

internal class TotalPriceCalculator
{
    public static decimal Calculate(ShoppingCart shoppingCart)
    {
        decimal totalPrice = 0;
        foreach (var item in shoppingCart.Items)
        {
            totalPrice += item.Price;
        }
        return totalPrice;

    }
}
