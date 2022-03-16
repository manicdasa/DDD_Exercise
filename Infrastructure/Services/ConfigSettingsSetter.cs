using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GhostWriter.Infrastructure.Identity
{
    public static class ConfigSettingsSetter
    {
        public static T Configure<T>(IConfiguration configuration, IServiceCollection services, string position) where T : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var jwtConfigSettings = new T();
            configuration.GetSection(position).Bind(jwtConfigSettings);
            services.AddSingleton(jwtConfigSettings);

            return jwtConfigSettings;
        }
    }
}
