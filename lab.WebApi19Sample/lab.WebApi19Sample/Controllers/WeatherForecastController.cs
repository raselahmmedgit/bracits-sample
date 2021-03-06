using lab.WebApi19Sample.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab.WebApi19Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : BaseApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                var rng = new Random();
                var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                _resultApi = ResultApi.Ok(data);
                return Ok(_resultApi);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(LogMessageHelper.FormateMessageForException(ex.Message, "Error"));
                return Error(ex);
            }
        }
    }
}
