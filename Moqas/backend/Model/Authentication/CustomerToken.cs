namespace Moqas.Model.Authentication
{
    public class CustomerToken
    {
        public int customerId { get; set; }
        public string? BrowserToken { get; set; }
        public DateTime? BrowserTokenExpires { get; set; }
    }
}
