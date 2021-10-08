using System.Collections.Generic;
using System.Threading.Tasks;
using Circuit.Breaker.Api.Data.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Circuit.Breaker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherServiceClient _weatherServiceClient; 

        public WeatherForecastController(IWeatherServiceClient weatherServiceClient)
        {
            _weatherServiceClient = weatherServiceClient;
        }
    
        [HttpGet]
        public Task<IEnumerable<WeatherForecast>> Get()
        {
            return _weatherServiceClient.GetListAsync();
        }
    }
}