using Npgsql;

namespace Discount.Grpc.Data
{
    public interface IDiscountDbContext
    {
        NpgsqlConnection ConnectionString { get; }
    }
}
