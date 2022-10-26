using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Symbols.Api.Hubs
{
    [Authorize]
    public class SymbolsHub : Hub
    {

    }
}
