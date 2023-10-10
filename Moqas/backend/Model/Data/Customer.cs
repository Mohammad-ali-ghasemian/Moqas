namespace Moqas.Model.Data
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? BrowserToken { get; set; }
        public DateTime? BrowserTokenExpires { get; set; }

        //navigation properties
        public List<Chat> Chat { get; set; }
    }
}
