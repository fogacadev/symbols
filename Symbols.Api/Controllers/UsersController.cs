using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using static Symbols.Api.AppConfiguration;

namespace Symbols.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<User>> GetAll([FromQuery] string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Ok(AppConfiguration.Users);
            }

            username = username.ToUpper();
            var users = AppConfiguration.Users.Where(u => u.Username.ToUpper().Contains(username));
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{username}")]
        public ActionResult<User> Get([FromRoute] string username)
        {
            var user = AppConfiguration.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

            if (user == null)
                return NotFound(new { Message = "Usuário não existe." });

            return Ok(user);
        }

    }
}
