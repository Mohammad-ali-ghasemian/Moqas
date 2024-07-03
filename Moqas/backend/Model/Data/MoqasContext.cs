using Microsoft.EntityFrameworkCore;

namespace Moqas.Model.Data
{
    public class MoqasContext : DbContext
    {
        public  DbSet<Customer>? Customers { get; set; }
        public  DbSet<Chat>? Chats { get; set; }
        public  DbSet<MessagesHistory>? MessagesHistory { get; set; }
        public  DbSet<CustomerSettings>? CustomerSettings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
            base.OnConfiguring(optionsBuilder);
        }

    }
}
