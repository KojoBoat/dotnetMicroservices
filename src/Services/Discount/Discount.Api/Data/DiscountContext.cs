using Npgsql;

namespace Discount.Api.Data
{
    public class DiscountContext : IDiscountDbContext
    {
        public DiscountContext(IConfiguration configuration)
        {
            ConnectionString = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
        public NpgsqlConnection ConnectionString { get; }
    }
}
