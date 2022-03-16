using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GhostWriter
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //var context = services.GetRequiredService<ApplicationDbContext>();

                    //if (context.Database.IsSqlServer())
                    //{
                    //    context.Database.Migrate();
                    //}

                    //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    //await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
                    //await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
                await host.RunAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry(o =>
                    {
                        o.Dsn = "https://71e5d7ec7b964c3eb6d41972ee2a29d4@o908955.ingest.sentry.io/5844457";
                        // When configuring for the first time, to see what the SDK is doing:
                        o.Debug = true;
                        // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                        // We recommend adjusting this value in production.
                        o.TracesSampleRate = 1.0;
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
