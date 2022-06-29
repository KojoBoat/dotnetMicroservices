using Discount.Grpc.Entities;

namespace Discount.Api.Repository
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
    }
}
