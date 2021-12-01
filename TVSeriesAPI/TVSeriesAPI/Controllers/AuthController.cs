using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVSeriesAPI.Authentication;
using TVSeriesAPI.Models.Auth;

namespace TVSeriesAPI.Controllers
{
    /// <summary>
    /// Contains actions intended to authenticate clients
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuth _jwtAuth;

        public AuthController(IJwtAuth jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }
        
        // POST api/auth/login
        /// <summary>
        /// Authenticates client using username and password
        /// Example body:
        /// {
        ///     "userName": "admin",
        ///     "password": "admin"
        /// }
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns> Authorization token if user credentials are correct, otherwise returns null</returns>
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
