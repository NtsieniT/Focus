using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Focus.Incident.API.StartUp
{
    public static partial class Extensions
    {
        public static IServiceCollection AddCustomConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected 
            //services.Configure<LDAPConnection>(configuration.GetSection("LDAP"));

            return services;
        }
    }
}
