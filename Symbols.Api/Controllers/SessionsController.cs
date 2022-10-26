using Microsoft.AspNetCore.Mvc;
using Symbols.Api.Services;
using Symbols.Api.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Symbols.Api.Controllers
{
    [ApiController]
    [Route("api/v1/sessions")]
    public class SessionsController : ControllerBase
    {
        public ActionResult LoginAsync([FromBody] LoginViewModel model)
        {
            var user = AppConfiguration
                .Users
                .FirstOrDefault(u => u.Username.ToLower() == model.Username.ToLower());

            if(user == null) 
                return BadRequest(new { Message = "Usuário não encontrado."});

            var token = TokenService.GenereateToken(user.Username, user.Name);

            return Ok(new LoginResponseViewModel { Token = token, User = user });
        }
    }
}
