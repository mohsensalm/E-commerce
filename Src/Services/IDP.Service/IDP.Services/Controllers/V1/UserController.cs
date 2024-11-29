 using Asp.Versioning;
using IDP.Api.Controllers.BaseController;
using IDP.Application.Comands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IDP.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion(2)] 
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/users")]

    public class UserController : IBaseController
    {
        private readonly IMediator _mediator; 
        public UserController(IMediator mediator )
        {
                _mediator =  mediator;
        }
        /// <summary>
        /// ورود اطلاعات کاربر   
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion(1)]        
        [HttpPost("Insert")]    
        public async Task<IActionResult> Insert([FromBody] UserComand userComand)
        {
            var res = await _mediator.Send(userComand);
            return Ok(res);
        }

    }
}
