using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;

namespace Discount.Api.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation($"Migration Postgresql Database Started at {DateTimeOffset.Now}");

                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand { Connection = connection };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"Create Table Coupon(
	                                              ID SERIAL PRIMARY KEY NOT NULL,
	                                              ProductName VARCHAR(24) NOT NULL,
	                                              Description TEXT,
	                                              Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Coupon (ProductName, Description,Amount) VALUES
                                            ('IPhone X', 'Iphone Discount', 150), ('Samsung 10', 'Samsung Discount', 100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation($"Migration Postgresql Database Finished at {DateTimeOffset.Now}");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, $"An error occured while migrating the postgresql database at {DateTimeOffset.Now}");

                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }                    
                }
            }
            return host;
        }
    }
}
