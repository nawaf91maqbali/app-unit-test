using AppUnitTest.Models;
using AppUnitTest.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AppUnitTest.Test
{
    /// <summary>
    /// Custom WebApplicationFactory for unit testing the API.
    /// </summary>
    public class CustomProgram : WebApplicationFactory<AppUnitTest.Program>
    {
        private readonly bool SeedDatabase = true;
        private readonly string DbName = "ApiUnitTesting";

        public CustomProgram(bool seedData = true) {
            SeedDatabase = seedData;
            DbName = $"ApiUnitTesting_{Guid.NewGuid()}"; // Ensure unique database name for each test run
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<DbContextService>(options =>
                {
                    options.UseInMemoryDatabase($"{DbName}");
                });

                // Register the IUserService with a mock or real implementation
                services.AddScoped<IUserService, UserService>();

                if (SeedDatabase)
                {
                    // Seed the in-memory database if needed
                    using (var serviceProvider = services.BuildServiceProvider()) // Fix: Use BuildServiceProvider from IServiceCollection
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<DbContextService>();
                        // Optionally, you can seed the database here
                        dbContext.Users.Add(User.TestUser());
                        dbContext.SaveChanges();
                    }
                }
            });
        }
    }
}
