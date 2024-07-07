using Moqas.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;

namespace Moqas.Tests.IntegrationTest
{
    public class MoqasControllerTests
    {
        [Fact]
        public async Task CustomerAuthenticationController_register()
        {
            // Arrange
            var application = new MoqasFactory();
            CustomerRegister request = A.Fake<CustomerRegister>();

            var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("api/CustomerAuthentication/register", request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CustomerAuthenticationController_ResetPassword()
        {
            // Arrange
            var application = new MoqasFactory();
            ResetPasswordRequest request = A.Fake<ResetPasswordRequest>();

            var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("api/CustomerAuthentication/reset-password", request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
