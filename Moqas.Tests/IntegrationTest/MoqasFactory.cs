using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Moqas.Model.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Moqas.Tests.IntegrationTest
{
    internal class MoqasFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<MoqasContext>));
                services.AddSqlServer<MoqasContext>("Server=.;Database=Moqas;Trusted_Connection=True;");

                var dbContext = CreateDBContext(services);
                dbContext.Database.EnsureDeleted();
            });

        }

        private static MoqasContext CreateDBContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MoqasContext>();
            return dbContext;
        }

    }
}
