using System.ComponentModel.DataAnnotations;

namespace Moqas.Model.Data
{
    public class MessagesHistory
    {
        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; } = false;

        //navigation properties
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
