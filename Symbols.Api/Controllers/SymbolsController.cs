using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Symbols.Api.Extensions;
using Symbols.Api.Hubs;
using Symbols.Api.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Symbols.Api.AppConfiguration;

namespace Symbols.Api.Controllers
{
    [ApiController]
    [Route("api/v1/symbols")]
    public class SymbolsController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostAsync(
            [FromBody] SendSymbomViewModel model,
            [FromServices] IHubContext<SymbolsHub> hub)
        {
            var username = User.IdentifierName();

            var pair = AppConfiguration
                .CommunicationPairs
                .FirstOrDefault(p => p.UsernameTwo == username || p.UsernameOne == username);

            if (pair == null)
            {
                return BadRequest(new { Message = "Erro, não foi possivel localizar par." });
            }

            var toUsername = pair.UsernameOne == username ? pair.UsernameTwo : pair.UsernameOne;

            var symbol = AppConfiguration.Symbols.FirstOrDefault(s => s.Code.ToLower() == model.Code.ToLower());

            await hub.Clients.User(toUsername).SendAsync("ReceiveSymbol", symbol);

            return NoContent();
        }

        [HttpGet("{code}")]
        [Authorize]
        public ActionResult<Symbol> Get([FromRoute] string code)
        {
            var symbol = AppConfiguration.Symbols.FirstOrDefault(u => u.Code.ToLower() == code.ToLower());
            if (symbol == null)
                return NotFound(new { Message = "Simbolo não encontrado." });

            return Ok(symbol);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Symbol>> GetAll([FromQuery] string query)
        {
            var symbols = AppConfiguration.Symbols;

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                symbols = symbols
                    .Where(s => s.Code.Contains(query) || s.Message.Contains(query))
                    .ToList();
            }

            return Ok(symbols);
        }
    }
}
