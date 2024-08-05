using Microsoft.AspNetCore.Mvc;

namespace CQRS.Ecommerce.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    public IActionResult Login()
    {
        return Ok();
    }
    public IActionResult Register()
    {
        return Ok();
    }
}
