using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Symbols.Api.Extensions;
using System.Linq;
using static Symbols.Api.AppConfiguration;

namespace Symbols.Api.Controllers
{
    [ApiController]
    [Route("api/v1/communicationpairs")]
    public class CommunicationPairsController : ControllerBase
    {
        [Authorize]
        public ActionResult<CommunicationPair> Get()
        {
            var username = User.IdentifierName();

            var pair = AppConfiguration.CommunicationPairs.FirstOrDefault(c => c.UsernameOne == username || c.UsernameTwo == username);

            if (pair == null)
                return NotFound(new { Message = "Par não configurado." });

            return Ok(pair);
        }
    }
}
