using DPS.Email.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace DPS.Email.Tests;

[TestClass]
public partial class BaseEmailBodyFactoryTest
{
    private static EmailBodyFactory _emailBodyFactory { get; set; }
    
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // Mock IServiceProvider
        var serviceProviderMock = new Mock<IServiceProvider>();
        // Mock ILoggerFactory
        var loggerFactoryMock = new Mock<ILoggerFactory>();
        // Mock ILogger<HtmlRendererWrapper>
        var loggerMock = new Mock<ILogger<HtmlRenderer>>();
        loggerFactoryMock
            .Setup(factory => factory.CreateLogger(It.IsAny<string>()))
            .Returns(loggerMock.Object);
        
        var htmlRenderer = new HtmlRenderer(serviceProviderMock.Object, loggerFactoryMock.Object);

        _emailBodyFactory = new EmailBodyFactory(htmlRenderer);
    }
}