using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Configurations;
using WeatherApp.Models;
using WeatherApp.Service;

namespace WeatherApp.Test
{
    [TestFixture]
    public class WeatherServiceTest
    {
        private WeatherService weatherService;
        private Mock<IHttpClientFactory> httpClientFactoryMock;
        private Mock<IOptions<OpenWeatherMapConfiguration>> optionsMock;
        private Mock<ILogger<WeatherService>> loggerMock;

        [SetUp]
        public void Setup()
        {
            httpClientFactoryMock = new Mock<IHttpClientFactory>();
            optionsMock = new Mock<IOptions<OpenWeatherMapConfiguration>>();
            loggerMock = new Mock<ILogger<WeatherService>>();

            weatherService = new WeatherService(httpClientFactoryMock.Object, optionsMock.Object, loggerMock.Object);
        }

        [Test]
        public async Task Get_ReturnsWeatherResponse_WhenApiCallSucceeds()
        {
            // Arrange
            var city = "London";
            var weatherResponse = new WeatherResponse
            {
                // Populate with appropriate data for the test
            };
            var jsonContent = JsonConvert.SerializeObject(weatherResponse);

            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonContent),
                });

            httpClientFactoryMock.Setup(factory => factory.CreateClient("OpenWeatherApiClient"))
                .Returns(httpClientMock.Object);

            // Act
            var result = await weatherService.Get(city);

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions based on the expected behavior
        }

        [Test]
        public async Task Get_ReturnsNull_WhenApiCallFails()
        {
           var city = "London";

            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound, // Simulate a failed response
                });

            httpClientFactoryMock.Setup(factory => factory.CreateClient("OpenWeatherApiClient"))
                .Returns(httpClientMock.Object);

            // Act
            var result = await weatherService.Get(city);

            // Assert
            Assert.IsNull(result);
            loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
        }

    }
}
