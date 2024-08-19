using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories;

// TODO: Create the Coupon Table using PostgreSQP docker file
// "CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(500) NOT NULL, Description TEXT, Amount INT)"
// "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Coupon', 500)"
// "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Coupon', 700)"

public class CouponRepository : ICouponRepository
{
    private readonly IConfiguration _configuration;

    public CouponRepository(IConfiguration configuration)
    {
        _configuration = configuration;
     }
    public async Task<Coupon> GetCoupon(string productName)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var getCouponQurey = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        var getCouponArgs = new { ProductName = productName };
        return await connection.QueryFirstOrDefaultAsync<Coupon>(getCouponQurey, getCouponArgs) ??
            new Coupon { ProductName = productName, Description = "No Coupon Available"};
    }

    public async Task<bool> CreateCoupon(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createCouponCommand = "INSERT INTO Coupon (ProductName, Description, Amount, IsActive) VALUES (@ProductName, @Description, @Amount, @IsActive)";
        var createCouponArgs = new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, IsActive = coupon.IsActive};
        return await connection.ExecuteAsync(createCouponCommand, createCouponArgs) is 0 ?
            false :
            true;
    }
    public async Task<bool> UpdateCoupon(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createCouponCommand = "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount, IsActive=@IsActive WHERE Id=@Id";
        var createCouponArgs = new { Id = coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };
        return await connection.ExecuteAsync(createCouponCommand, createCouponArgs) is 0 ?
            false :
            true;
    }

    public async Task<bool> DeleteCoupon(string couponId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createCouponCommand = "UPDATE Coupon SET IsActive=@IsActive WHERE Id=@Id";
        var createCouponArgs = new { Id = couponId , IsActive = false };
        return await connection.ExecuteAsync(createCouponCommand, createCouponArgs) is 0 ?
            false :
            true;
    }

}
