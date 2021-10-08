using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Circuit.Breaker.Api.Data.Clients
{
    public interface IWeatherServiceClient
    {
        Task<IEnumerable<WeatherForecast>> GetListAsync();
    }
    public class WeatherServiceClient : IWeatherServiceClient
    {
        private const string WeatherForecastPath = "WeatherForecast";
        private readonly HttpClient _client;

        public WeatherServiceClient(HttpClient client)
        {
            _client = client;
        }
        public async  Task<IEnumerable<WeatherForecast>> GetListAsync()
        {
            var response = await _client.GetFromJsonAsync<List<WeatherForecast>>(WeatherForecastPath);
            return response;
        }
    }

 
}