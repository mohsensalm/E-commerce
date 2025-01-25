using Asp.Versioning;
using IDP.Application.Comands.Auth;
using IDP.Application.Comands.User;
using IDP.Application.Querys.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{ 
    [ApiController]
    [ApiVersion(2)]
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthQuery authQuery)
        {
            var res = await _mediator.Send(authQuery);
            return this.Ok(res);
        }

        [HttpPost("RegisterOtp")]
        public async Task<IActionResult> RegisterAndSendOTP([FromBody] AuthComand authComand)
        {
            await Console.Out.WriteLineAsync("");

            var res = await _mediator.Send(authComand);
            return Ok(res);
        }

    }
}
