namespace Moqas.Model.Data
{
    public class Chat
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; } = false;

        //navigation properties
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<MessagesHistory> MessagesHistory { get; set; }
    }
}
