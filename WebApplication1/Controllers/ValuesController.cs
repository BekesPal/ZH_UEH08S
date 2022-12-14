using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("questions/all")]

        public IActionResult Függvény()
        {
            Models.HajosContext context = new Models.HajosContext();
            var kerdesek = from x in context.Questions select x.Question1;
            return Ok(kerdesek);
        }
    }
}
