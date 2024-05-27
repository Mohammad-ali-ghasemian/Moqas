namespace Moqas.Model.Profile
{
    public class Profile
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string WebsiteLink { get; set; } = string.Empty;
        public string ConfigUsername { get; set; } = string.Empty;
        public DateTime? ConfigCreatedAt { get; set; }
        public DateTime? ConfigExpires { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string? BrowserToken { get; set; }
        public DateTime? BrowserTokenExpires { get; set; }
    }
}
