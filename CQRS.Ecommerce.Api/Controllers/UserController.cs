using Microsoft.AspNetCore.Mvc;

namespace CQRS.Ecommerce.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{

    // Create User from Auth Controller
    public IActionResult GetAllUser()
    {
        return Ok();
    }
    public IActionResult GetUserById()
    {
        return Ok();
    }
    public IActionResult UpdateUser()
    {
        return Ok();
    }
    public IActionResult DeleteUser()
    {
        return Ok();
    }
    public IActionResult ChangeToVendor()
    {
        return Ok();
    }
    public IActionResult ChangeToMember()
    {
        return Ok();
    }
}
