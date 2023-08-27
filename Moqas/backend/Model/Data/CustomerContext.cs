using Microsoft.EntityFrameworkCore;

namespace Moqas.Model.Data
{
    public class CustomerContext : DbContext
    {
        public  DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=Moqas;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
