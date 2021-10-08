using System;
using Circuit.Breaker.Api.Data.Clients;
using Circuit.Breaker.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Circuit.Breaker.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLaunchDarkly()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Circuit.Breaker2.Api", Version = "v1"});
                });
        
            services.AddHttpClient<IWeatherServiceClient, WeatherServiceClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5001");
                client.Timeout = TimeSpan.FromSeconds(5);
            }).AddSwitchableCircuitBreaker();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Circuit.Breaker2.Api v1"));
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}