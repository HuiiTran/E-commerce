using JWTAuthenManager;
using JWTAuthenManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("Authen")]
    public class AuthController : ControllerBase
    {
        private readonly JWTTokenHandler jwtTokenHandler;

        public AuthController(JWTTokenHandler jwtTokenHandler)
        {
            this.jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest request)
        {
            var authenticationResponse = jwtTokenHandler.GenerateJwtToken(request);

            if (authenticationResponse == null)
            {
                return Unauthorized();
            }

            return authenticationResponse;
        }
    }
}
