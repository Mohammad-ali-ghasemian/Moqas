namespace Moqas.Model.Chat
{
    public class Chat
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; } = false;
        public int CustomerId { get; set; }
    }
}
