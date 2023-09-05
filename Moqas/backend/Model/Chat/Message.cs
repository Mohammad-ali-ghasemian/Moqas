namespace Moqas.Model.Chat
{
    public class Message
    {
        public long Id { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
