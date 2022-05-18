using Npgsql;

namespace Discount.Api.Data
{
    public interface IDiscountDbContext
    {
        NpgsqlConnection ConnectionString { get; }
    }
}
