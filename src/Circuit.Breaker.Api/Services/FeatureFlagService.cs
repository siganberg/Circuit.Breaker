using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server.Interfaces;

namespace Circuit.Breaker.Api.Services
{
    public interface IFeatureFlagService
    {
        bool IsCircuitBreakerEnabled { get; }
    }
    
    public class FeatureFlagService : IFeatureFlagService
    {
        public const string CircuitBreakerDemo = "circuit-breaker-demo";

        private readonly ILdClient _ldClient;
        public FeatureFlagService( ILdClient ldClient)
        {
            _ldClient = ldClient;
        }
        
        private bool GetFeatureFlag(string flagName, User ldUser, bool defaultValue = false)
        {
            return _ldClient.BoolVariation(flagName, ldUser, defaultValue);
        }

        public bool IsCircuitBreakerEnabled => GetFeatureFlag("circuit-breaker-demo", User.WithKey(string.Empty));
       
    }
}