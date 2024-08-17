using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories;

// TODO: Create the Coupon Table using PostgreSQP docker file
// "CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(500) NOT NULL, Description TEXT, Amount INT)"
// "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500)"
// "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700)"

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var getDiscountQurey = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        var getDiscountArgs = new { ProductName = productName };
        return await connection.QueryFirstOrDefaultAsync<Coupon>(getDiscountQurey, getDiscountArgs) ??
            new Coupon { ProductName = productName, Description = "No Discount Available"};
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createDiscountCommand = "INSERT INTO Coupon (ProductName, Description, Amount, IsActive) VALUES (@ProductName, @Description, @Amount, @IsActive)";
        var createDiscountArgs = new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, IsActive = coupon.IsActive};
        return await connection.ExecuteAsync(createDiscountCommand, createDiscountArgs) is 0 ?
            false :
            true;
    }
    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createDiscountCommand = "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount, IsActive=@IsActive WHERE Id=@Id";
        var createDiscountArgs = new { Id = coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
        return await connection.ExecuteAsync(createDiscountCommand, createDiscountArgs) is 0 ?
            false :
            true;
    }

    public async Task<bool> DeleteDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createDiscountCommand = "UPDATE Coupon SET IsActive=@IsActive WHERE Id=@Id";
        var createDiscountArgs = new { Id = coupon.Id , IsActive = false };
        return await connection.ExecuteAsync(createDiscountCommand, createDiscountArgs) is 0 ?
            false :
            true;
    }

}
