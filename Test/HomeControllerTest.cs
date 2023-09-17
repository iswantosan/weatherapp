using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeatherApp.Controllers;
using WeatherApp.Models;
using WeatherApp.Service;

[TestFixture]
public class HomeControllerTests
{
    private HomeController homeController;
    private Mock<IWeatherService> weatherServiceMock;
    private Mock<ILogger<HomeController>> loggerMock;

    [SetUp]
    public void Setup()
    {
        weatherServiceMock = new Mock<IWeatherService>();
        loggerMock = new Mock<ILogger<HomeController>>();

        homeController = new HomeController(
            countryService: null,
            cityService: null,
            weatherService: weatherServiceMock.Object,
            logger: loggerMock.Object
        );
    }

    [Test]
    public async Task WeatherAsync_ReturnsJsonResult_WhenWeatherServiceSucceeds()
    {
        // Arrange
        var city = "London";
        var weatherResponse = new WeatherResponse
        {

        };

        weatherServiceMock.Setup(service => service.Get(city))
            .ReturnsAsync(weatherResponse);

        // Act
        var result = await homeController.WeatherAsync(city) as JsonResult;

        // Assert
        Assert.IsNotNull(result);
        dynamic data = result.Value;
        Assert.IsTrue(data.status);
        Assert.IsNotNull(data.data);
        loggerMock.VerifyNoOtherCalls(); // Verify that no error log was called
    }

    [Test]
    public async Task WeatherAsync_ReturnsJsonResultWithStatusFalse_WhenWeatherServiceFails()
    {
        // Arrange
        var city = "London";

        weatherServiceMock.Setup(service => service.Get(city))
            .ReturnsAsync((WeatherResponse)null);

        // Act
        var result = await homeController.WeatherAsync(city) as JsonResult;

        // Assert
        Assert.IsNotNull(result);
        dynamic data = result.Value;
        Assert.IsFalse(data.status);
        Assert.IsNull(data.data);
        loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
    }
}