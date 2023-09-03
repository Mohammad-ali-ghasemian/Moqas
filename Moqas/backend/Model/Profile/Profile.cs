namespace Moqas.Model.Profile
{
    public class Profile
    {
        public int CustomerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime? VerifiedAt { get; set; }
    }
}
