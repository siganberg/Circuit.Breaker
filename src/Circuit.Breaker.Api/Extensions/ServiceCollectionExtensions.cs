using Circuit.Breaker.Api.Services;
using LaunchDarkly.Sdk.Server;
using LaunchDarkly.Sdk.Server.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Circuit.Breaker.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLaunchDarkly(this IServiceCollection services)
        {
            services.AddSingleton<IFeatureFlagService, FeatureFlagService>();
            services.AddTransient(provider =>
                {
                    var config = provider.GetRequiredService<IConfiguration>();
                    var key = config["LaunchDarkly:Configs:Checkout:SdkKey"];
                    return Configuration.Default(key);
                })
                .AddSingleton<ILdClient, LdClient>();
            return services;
        }
      
        
        
    }
}