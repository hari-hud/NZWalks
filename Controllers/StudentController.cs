using Microsoft.AspNetCore.Mvc;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        String[] students = new String[] {
            "Hari", "Jayshree", "Maroti", "Shriram"
        };
        return Ok(students);
    }
}
