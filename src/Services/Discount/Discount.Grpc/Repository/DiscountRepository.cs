using Dapper;
using Discount.Grpc.Data;
using Discount.Grpc.Entities;

namespace Discount.Api.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountDbContext _context;
        public DiscountRepository(IDiscountDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = _context.ConnectionString;
            var result = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { coupon.ProductName, coupon.Description, coupon.Amount });
            return result != 0;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = _context.ConnectionString;
            var discountToDelete = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductName = @ProductName", new { productName });

            return discountToDelete != 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = _context.ConnectionString;
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName});

            return coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc." };
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = _context.ConnectionString;
            var discountToUpdate = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                    new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });

            return discountToUpdate != 0;
        }
    }
}
