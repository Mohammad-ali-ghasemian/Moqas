using Moqas.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Moqas.Tests.IntegrationTest
{
    public class MoqasControllerTests
    {
        [Fact]
        public async Task CustomerAuthenticationController_register()
        {
            // Arrange
            var application = new MoqasFactory();
            CustomerRegister request = new CustomerRegister
            {
                Email = "test@gmail.com",
                Password = "password",
                ConfirmPassword = "password",
                WebsiteLink = "test.com"
            };

            var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("api/CustomerAuthentication/register", request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
