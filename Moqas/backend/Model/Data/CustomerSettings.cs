namespace Moqas.Model.Data
{
    public class CustomerSettings
    {
        public enum TypeStyle
        {
            FAQ,
            UI,
            TOUR
        }

        public int Id { get; set; }
        public TypeStyle Type {  get; set; }
        public string? Key { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;

        //navigation properties
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
