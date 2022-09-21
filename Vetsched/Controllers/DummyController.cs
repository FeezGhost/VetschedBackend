using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vetsched.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DummyController : ControllerBase
    {
        [HttpGet("hi")]
        public async Task<IActionResult> AuthPing()
        {
            var stringNnn = "hello";
            return Ok(stringNnn);
        }
    }
}
