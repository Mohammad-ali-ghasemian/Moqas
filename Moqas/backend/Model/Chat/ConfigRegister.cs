using System.ComponentModel.DataAnnotations;

namespace Moqas.Model.Authentication
{
    public class ConfigRegister
    {
        [Required]
        public string BrowserToken { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
