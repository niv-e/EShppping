namespace Cart.Core.Entities;

public record ShoppingCart(string UserName)
{
    public ShoppingCart(string userName, IEnumerable<ShoppingCartItem> items) : this(userName)
    {
        this.Items = items;
    }

    public IEnumerable<ShoppingCartItem> Items { get; set; } = Enumerable.Empty<ShoppingCartItem>();
}