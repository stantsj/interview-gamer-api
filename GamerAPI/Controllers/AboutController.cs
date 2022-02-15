using Microsoft.AspNetCore.Mvc;

namespace GamerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AboutController : Controller
    {
        // GET: /Games
        [HttpGet]
        public IActionResult GetAbout()
        {
            return Ok(
                new { message = "Welcome to the GamerAPI! All game data is provided by RAWG https://rawg.io/" }
            );
        }
    }
}
