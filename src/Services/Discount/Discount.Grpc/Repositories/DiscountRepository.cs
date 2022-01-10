using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private string GetConnection => _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(GetConnection);
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            if (coupon is null) return new() { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(GetConnection);
            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            return affected > 0;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(GetConnection);
            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount Where Id = @Id", new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });
            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(GetConnection);
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName});
            return affected > 0;
        }
    }
}
