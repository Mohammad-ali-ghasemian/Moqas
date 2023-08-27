using System.ComponentModel.DataAnnotations;

namespace Moqas.Model
{
    public class CustomerLogin
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
