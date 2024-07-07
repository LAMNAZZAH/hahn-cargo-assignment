using Microsoft.AspNetCore.Mvc;
namespace ComplimentGeneratorAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ComplimentController : ControllerBase
  {
    private static readonly string[] Compliments = new[]
    {
      "You'r!",
          };

  [HttpGet]
  public ActionResult<string> GetRandomCompliment()
  {
    Random random = new Random();
    int index = random.Next(Compliments.Length);
     return Ok(Compliments[index]);
   }
  }
}