using System.ComponentModel.DataAnnotations;

namespace Symbols.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required( ErrorMessage = "Preencha o username.")]
        public string Username { get; set; }
    }
}
