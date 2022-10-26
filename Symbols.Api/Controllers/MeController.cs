using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Symbols.Api.Extensions;
using System.Linq;
using static Symbols.Api.AppConfiguration;

namespace Symbols.Api.Controllers
{
    [ApiController]
    [Route("api/v1/me")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            var username = User.IdentifierName();

            var user = AppConfiguration.Users.FirstOrDefault(u => u.Username == username);

            return Ok(user);
        }

        
    }
}
