﻿using Microsoft.AspNetCore.Mvc;
using PasswordHasher.Core;
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
    }
}