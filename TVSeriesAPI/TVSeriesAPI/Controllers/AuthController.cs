using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.Authentication;
using TVSeriesAPI.Models.Auth;

namespace TVSeriesAPI.Controllers
{
    /// <summary>
    /// Contains actions intended to authenticate clients
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuth _jwtAuth;

        public AuthController(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }

        /// <summary>
        /// Authenticates client using username and password
        /// </summary>
        /// <returns>Status code, and authorization token if user credentials are correct</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /auth/login
        ///     {
        ///     "userName": "admin",
        ///     "password": "admin"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">If authorization token is returned</response>
        /// <response code="401">If login credentials are incorrect</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // POST auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = _jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
