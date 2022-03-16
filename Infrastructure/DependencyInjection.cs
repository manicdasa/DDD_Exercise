using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GhostWriter.Infrastructure.Persistence;
using GhostWriter.Infrastructure.Identity;
using GhostWriter.Domain.Entities;
using GhostWriter.Application.Common.Interfaces;
using GhostWriter.Infrastructure.Services;
using GhostWriter.Infrastructure.Settings;
using Braintree;
using GhostWriter.Application.Common.Services;

namespace GhostWriter.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Entities>(options => 
                        options.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("ConnStr"),
                        x => x.MigrationsAssembly("Infrastructure"))
                        );

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<Entities>()
                .AddDefaultTokenProviders();

            ConfigSettingsSetter.Configure<JWTConfigSetting>(configuration, services, "JWT");
            ConfigSettingsSetter.Configure<SMPTConfigSettings>(configuration, services, "SMPT");
            ConfigSettingsSetter.Configure<PayPalConfigSettings>(configuration, services, "PayPal");
            ConfigSettingsSetter.Configure<EnvironmentConfigSettings>(configuration, services, "Environment");
            var braintreeConfig = ConfigSettingsSetter.Configure<BraintreeConfigSettings>(configuration, services, "Braintree");
            ConfigSettingsSetter.Configure<CopyLeaksConfigSettings>(configuration, services, "CopyLeaks");

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddTransient<IUserManagementFactory, UserManagementFactory>();
            services.AddTransient<IFileProvider, FileProvider>();
            services.AddTransient<IPictureService, PictureService>();
            services.AddTransient<IWordGenerator, WordGenerator>();
            services.AddTransient<IEmailer, Emailer>();
            services.AddTransient<IConversationService, ConversationService>();
            services.AddTransient<IPlagiarismChecker, CopyleaksPlagiarismChecker>();
            services.AddTransient<IPayoutService, PayPalPayoutService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IBraintreeService, Services.BraintreeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<Entities>());


            services.AddSingleton<IBraintreeGateway, BraintreeGateway>(provider => new BraintreeGateway(braintreeConfig.Environment, braintreeConfig.MerchantId, braintreeConfig.PublicKey, braintreeConfig.PrivateKey));
            
            return services;
        }
    }
}
