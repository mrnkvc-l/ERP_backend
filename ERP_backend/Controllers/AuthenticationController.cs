using ERP_backend.Model;
using ERP_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        [Consumes("application/json")]
        public IActionResult Authenticate([FromBody] UserAuthenticationDTO userAuthenticationDTO)
        {
            if (authenticationRepository.AuthenticateUser(userAuthenticationDTO))
                return Ok(new { token = authenticationRepository.GenerateJWT(userAuthenticationDTO.Username) });
            return Unauthorized("User not authorized.");
        }

        [HttpGet("renewAuth")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RenewAuth()
        {
            return Ok(new { token = authenticationRepository.GenerateJWT(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) });
        }

    }
}
