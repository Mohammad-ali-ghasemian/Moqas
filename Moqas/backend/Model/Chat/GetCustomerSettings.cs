using static Moqas.Model.Data.CustomerSettings;

namespace Moqas.Model.Chat
{
    public class GetCustomerSettings
    {
        public int CustomerId { get; set; }
        public TypeStyle Type { get; set; }
        public string? Key { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;
    }
}
