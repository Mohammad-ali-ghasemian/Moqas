using System.ComponentModel.DataAnnotations;

namespace Moqas.Model.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        [DataType(DataType.Url)]
        public string WebsiteLink { get; set; } = string.Empty;
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? BrowserToken { get; set; }
        public DateTime? BrowserTokenExpires { get; set; }

        public string ConfigUsername { get; set; } = string.Empty;
        public byte[] ConfigPasswordHash { get; set; } = new byte[32];
        public byte[] ConfigPasswordSalt { get; set; } = new byte[32];
        public DateTime? ConfigCreatedAt { get; set; }

        //navigation properties
        public List<Chat> Chat { get; set; }
    }
}
