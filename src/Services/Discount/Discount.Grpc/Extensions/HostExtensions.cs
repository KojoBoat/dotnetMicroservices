using Npgsql;

namespace Discount.Grpc.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost Host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            var scope = Host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrating database to Postgres");

                using var connection = new NpgsqlConnection
                    (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection,
                };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(
		                                ID SERIAL PRIMARY KEY NOT NULL,
		                                ProductName VARCHAR(24) NOT NULL,
		                                Description TEXT,
		                                Amount INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Samsung 10', 'Samsung Discount', 100);";
                command.ExecuteNonQuery();
                
                command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('MacBook', 'MacBook Discount', 10);";
                command.ExecuteNonQuery();

                logger.LogInformation("Successfuly migrated Postgres Database");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occured.");

                if(retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(Host, retryForAvailability);
                }
            }

            return Host;
        }
    }
}
