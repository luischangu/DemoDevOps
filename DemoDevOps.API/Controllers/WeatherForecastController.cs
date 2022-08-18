using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace DemoDevOps.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly IHub _sentryHub;


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHub sentryHub)
        {
            _logger = logger;
            _sentryHub = sentryHub;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //HttpGet

        [HttpGet("Test")]
        public IActionResult Test() {
            SentrySdk.CaptureMessage("Hello Sentry");

            try
            {
                var num1 = 0;
                var num2 = 14;

                var result = num2 / num1;
            }
            catch (Exception ex )
            {
                _sentryHub.CaptureException(ex);

                return BadRequest();
            }
            return Ok();
        }
        

              
        
    }
}