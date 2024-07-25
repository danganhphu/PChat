using Microsoft.AspNetCore.Mvc;

namespace PChat.WebAPI.Controller;

public class Controller1 : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}