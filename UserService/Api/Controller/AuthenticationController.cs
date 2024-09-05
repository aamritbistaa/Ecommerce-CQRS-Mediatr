using Application.Common;
using Application.DTO;
using Application.Manager.Implementation;
using Application.Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationImplementaton _manager;

        public AuthenticationController(IAuthenticationImplementaton manager)
        {
            _manager = manager;
        }


        [HttpPost("RequestToVerifyUser")]
        public async Task<ServiceResult<string>> RequestToVerifyUser(string mailingAddress)
        {
            var result = await _manager.RequestToVerifyUser(mailingAddress);
            return result;
        }
        [HttpPost("VerifyUser")]
        public async Task<ServiceResult<string>> VerifyUser(VerifyUserRequestDto request)
        {
            var result = await _manager.VerifyUser(request);
            return result;
        }
    }
}
