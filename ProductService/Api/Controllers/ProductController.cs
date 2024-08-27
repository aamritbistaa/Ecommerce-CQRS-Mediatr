using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("ProductTestApi")]
        public async Task<string> ProductTestApi()
        {
            return "Test";
        }
    }
}
