using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moqas.Controllers.Authentication;
using Moqas.Controllers.Chat;
using Moqas.Model.Authentication;
using Moqas.Model.Chat;
using Moqas.Model.Data;
using Moqas.Model.Settings;

namespace Moqas.Tests.Controller
{
    public class MoqasControllerTests
    {
        MoqasContext _context;
        public MoqasControllerTests()
        {
            _context = A.Fake<MoqasContext>();
        }

        [Fact]
        public void CustomerAuthenticationController_Login_ReturnOK()
        {
            //Arrange
            CustomerLogin request = A.Fake<CustomerLogin>();
            var controller = new CustomerAuthenticationController(_context);

            //Act
            var result = controller.Login(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));
        }

        [Fact]
        public void ConfigController_ConfigRegister_ReturnOK()
        {
            //Arrange
            ConfigRegister request = A.Fake<ConfigRegister>();
            var controller = new ConfigController(_context);

            //Act
            var result = controller.ConfigRegister(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));
        }

        [Fact]
        public void CustomerSettingsController_InsertSetting_ReturnOK()
        {
            //Arrange
            GetCustomerSettings settings = A.Fake<GetCustomerSettings>();
            var controller = new CustomerSettingsController(_context);

            //Act
            var result = controller.InsertSetting(settings);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));
        }
    }
}
