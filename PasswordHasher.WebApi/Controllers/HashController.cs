using System.Net;
using Microsoft.AspNetCore.Mvc;
using PasswordHasher.Core.Jobs;
using PasswordHasher.WebApi.Models;

namespace PasswordHasher.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HashController : ControllerBase
    {
        private IJobEngine _jobEngine;

        public HashController(IJobEngine jobEngine)
        {
            _jobEngine = jobEngine;
        }

        [HttpGet("{jobId}")]
        public OkObjectResult GetByJobId(int jobId)
        {
            var jobResult = _jobEngine.GetJobResult(jobId);
            return Ok(jobResult);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateHashRequest request)
        {
            var jobId = _jobEngine.StartJob(request.Password);
            if (!jobId.HasValue)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }
            return Accepted(jobId);
        }
    }
}
