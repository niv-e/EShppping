namespace Cart.Core.Entities;

public record ShoppingCart
{
    public ShoppingCart(){ }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart(string userName, IEnumerable<ShoppingCartItem> items) : this(userName)
    {
        this.Items = items;
    }
    public string? UserName { get; init; }
    public IEnumerable<ShoppingCartItem> Items { get; set; } = Enumerable.Empty<ShoppingCartItem>();
}