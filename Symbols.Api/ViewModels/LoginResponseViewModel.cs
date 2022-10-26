using static Symbols.Api.AppConfiguration;

namespace Symbols.Api.ViewModels
{
    public class LoginResponseViewModel
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
