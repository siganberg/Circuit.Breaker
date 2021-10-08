using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MakeItBusyController : Controller
    { 
        public static bool BusyState = false;

        private readonly ILogger<MakeItBusyController> _logger;

        public MakeItBusyController(ILogger<MakeItBusyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get(bool makeItBusy)
        {
            BusyState = makeItBusy;
            _logger.LogInformation($"Busy state: {BusyState}");
            return true; 
        }
        
    }
}