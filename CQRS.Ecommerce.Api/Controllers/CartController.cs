using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Ecommerce.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : Controller
{
    public IActionResult ViewCart()
    {
        return Ok();
    }
    public IActionResult CreateCart()
    {
        return Ok();
    }
    public IActionResult AddItemToCart()
    {
        return Ok();
    }
    public IActionResult RemoveItemFromCart()
    {
        return Ok();
    }
}
