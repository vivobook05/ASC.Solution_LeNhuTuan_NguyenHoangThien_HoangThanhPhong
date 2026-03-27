using ASC.Web.Configuration;
using ASC.Web.Controllers;
using ASC.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ASC.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IOptions<ApplicationSettings>> optionsMock;
        private readonly Mock<ILogger<HomeController>> loggerMock;
        private readonly Mock<IEmailSender> emailSenderMock;

        public HomeControllerTests()
        {
            loggerMock = new Mock<ILogger<HomeController>>();
            emailSenderMock = new Mock<IEmailSender>();

            optionsMock = new Mock<IOptions<ApplicationSettings>>();
            optionsMock.Setup(ap => ap.Value).Returns(new ApplicationSettings
            {
                ApplicationTitle = "ASC"
            });
        }

        [Fact]
        public void HomeController_Index_View_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);

            Assert.IsType<ViewResult>(controller.Index(emailSenderMock.Object));
        }

        [Fact]
        public void HomeController_Index_NoModel_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);

            Assert.Null((controller.Index(emailSenderMock.Object) as ViewResult).ViewData.Model);
        }

        [Fact]
        public void HomeController_Index_Validation_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);

            Assert.Equal(0, (controller.Index(emailSenderMock.Object) as ViewResult).ViewData.ModelState.ErrorCount);
        }

        [Fact]
        public void HomeController_Index_Session_Test()
        {
            var controller = new HomeController(loggerMock.Object, optionsMock.Object);

            var result = controller.Index(emailSenderMock.Object) as ViewResult;
            Assert.NotNull(result);
        }
    }
}