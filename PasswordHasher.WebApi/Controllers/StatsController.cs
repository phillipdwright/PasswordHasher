using Microsoft.AspNetCore.Mvc;
using PasswordHasher.Core.Stats;
using PasswordHasher.WebApi.Models;

namespace PasswordHasher.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private IStatsProvider _statsProvider;

        public StatsController(IStatsProvider statsProvider)
        {
            _statsProvider = statsProvider;
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            var stats = new StatsResponse
            {
                Total = _statsProvider.GetJobCount(),
                Average = _statsProvider.GetAverageProcessTimeInMilliseconds()
            };
            return Ok(stats);
        }
    }
}
