using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR;

namespace LongRunningTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly BackgroundWorkerQueue _backgroundWorkerQueue;
        private IHubContext<LocationHub, ILocationHubService> _hubContext;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, BackgroundWorkerQueue backgroundWorkerQueue, IHubContext<LocationHub, ILocationHubService> hubContext)
        {
            _logger = logger;
            _backgroundWorkerQueue = backgroundWorkerQueue;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Callslowapi();
            return Ok();
        }

        private void Callslowapi()
        {
            _logger.LogInformation($"Starting at {DateTime.UtcNow.TimeOfDay}");
            _backgroundWorkerQueue.QueueBackgroundWorkItem(async token =>
            {
                await Task.Delay(60000);
                await _hubContext.Clients.All.GetLocation("sucess");
                _logger.LogInformation($"Done at {DateTime.UtcNow.TimeOfDay}");
            });
            
        }

       
    }
}
