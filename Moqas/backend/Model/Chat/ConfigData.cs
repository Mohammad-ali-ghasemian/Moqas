namespace Moqas.Model.Chat
{
    public class ConfigData
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string BrowserToken { get; set; } = string.Empty;
        public string WebsiteLink { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime? ConfigCreatedAt { get; set; }
        public DateTime? ConfigExpires { get; set; }
    }
}
