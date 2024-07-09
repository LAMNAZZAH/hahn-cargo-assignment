using Microsoft.AspNetCore.Mvc;
namespace ComplimentGeneratorAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComplimentController : ControllerBase
  {
    private static readonly string[] Compliments = new[]
    {
      "You'r!",
          };

  [HttpGet("random")]
  public ActionResult<string> GetRandomCompliment()
  {
    Random random = new Random();
    int index = random.Next(Compliments.Length);
     return Ok(Compliments[index]);
   }
  }
}