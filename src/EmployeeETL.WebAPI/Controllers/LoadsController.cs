using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/loads")]
public class LoadsController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        if (id != 1)
            return NotFound();

        return Ok('1');
    }

    [HttpPost]
    public ActionResult Post()
    {
        return BadRequest();
    }
}