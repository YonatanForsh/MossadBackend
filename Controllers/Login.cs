using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MossadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        //חיבור על ידי שם משתמש
        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateLogin()
        {
            return StatusCode(StatusCodes.Status201Created,
                new { token = "khkgyuk" });
        }
    }
}
