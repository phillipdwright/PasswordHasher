using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PasswordHasher.Core.Jobs;

namespace PasswordHasher.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShutdownController : ControllerBase
    {
        private IJobBag _jobBag;

        public ShutdownController(IJobBag jobBag)
        {
            _jobBag = jobBag;
        }

        [HttpPost]
        public async Task<ActionResult> ShutDown()
        {
            await _jobBag.AwaitRemainingAsync();
            Program.ShutDown();
            return Ok();
        }
    }
}
