using System;
using System.Net.Http;
using System.Threading.Tasks;
using Circuit.Breaker.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Serilog;

namespace Circuit.Breaker.Api
{
    public static class HttpClientBuilderExtensions
    {
        private static bool? _circuitBreaker; 
        public static IHttpClientBuilder AddSwitchableCircuitBreaker(this IHttpClientBuilder builder)
        {
            var noOpPolicy = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TaskCanceledException>()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10), OnBreak, OnReset, OnHalfOpen);
            
            builder.AddPolicyHandler((serviceProvider, _) =>
            {
                var featureFlag = serviceProvider.GetRequiredService<IFeatureFlagService>();
                var currentState = featureFlag.IsCircuitBreakerEnabled;
                if (_circuitBreaker != currentState)
                {
                    _circuitBreaker = currentState;
                    Log.Logger.Information($"Circuit Breaker is {(currentState ? "enabled" : "disabled")}.");
                }
                return currentState ? circuitBreakerPolicy : noOpPolicy;
            });
            return builder;
        }
        
        private static void OnReset()
        {
            Log.Logger.Information("Circuit Breaker: Reset");
        }

        private static void OnHalfOpen()
        {
            Log.Logger.Error("Circuit Breaker: Half-Open");
        }
        
        private static void OnBreak(DelegateResult<HttpResponseMessage> arg1, TimeSpan arg2)
        {
            Log.Logger.Error("Circuit Breaker: Break");
        }
    }
}