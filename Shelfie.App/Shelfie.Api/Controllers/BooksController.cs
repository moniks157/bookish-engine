using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shelfie.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok("Hello World");
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Hello World");
    }
}
