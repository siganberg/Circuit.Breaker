using System;
using System.Threading.Tasks;
using NBomber.CSharp;
using NBomber.Plugins.Http.CSharp;
using NBomber.Plugins.Network.Ping;

namespace Circuit.Breaker.LoadTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var rate = args == null || args.Length == 0  ? 1  : int.Parse(args[0]);
            var duration = args == null || args.Length < 2  ? 10  : int.Parse(args[1]);
          
            var step = Step.Create("WeatherForecast",
                HttpClientFactory.Create(),
                context =>
                { 
                    Task.Delay(1000);
                    var request = Http.CreateRequest("GET", $"http://localhost:5000/WeatherForecast");
                    return Http.Send(request, context);
                }, TimeSpan.FromMilliseconds(3000));

            var scenario = ScenarioBuilder
                .CreateScenario("simple_http", step)
                .WithWarmUpDuration(TimeSpan.FromSeconds(1))
                .WithLoadSimulations(
                    Simulation.InjectPerSec(rate, TimeSpan.FromSeconds(duration))
                );

            var pingPluginConfig = PingPluginConfig.CreateDefault(new[] {"http://localhost:5000"});
            var pingPlugin = new PingPlugin(pingPluginConfig);                

            NBomberRunner
                .RegisterScenarios(scenario) 
                .WithWorkerPlugins(pingPlugin)
                .Run();
        }
    }
}