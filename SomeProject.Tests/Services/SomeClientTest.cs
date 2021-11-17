using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SomeProject.Services;
using Xunit;

namespace SomeProject.Tests.Services
{
    public class SomeClientTest
    {
        //!ОСТОРОЖНО!, много плохих практик в одном месте
        //Чтобы избежать сложных тестов классов, использующих HttpClient, лучше разделить логику и сами вызовы
        //Это позволит протестировать логику отдельно и свести к минимуму тесты HttpClient-а
        //Если ваши тесты стали похожи на те, что ниже, скорее всего что-то в дизайне приложения пошло не так



        //Happy path
        [Fact]
        public async Task Some_client_returns_correct_data()
        {
            // Arrange
            var content = new StringContent("{\"Date\":\"TestDate\"}");
            var settings = new ClientSettings
            {
                ApiUrl = "https://www.google.com",
                TimeZone = "TestZone"
            };
            var handlerMock = new Mock<TestHttpMessageHandler>{CallBase = true};
            using var httpClient = new HttpClient(handlerMock.Object);
            //На примере старых библиотечных классов видно, почему отсутствие абстрации усложняет процесс юнит-тестирования
            handlerMock
                .Setup(handler => handler.SendTestAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK, 
                    Content = content
                });
            
            var someClient = new SomeClient(httpClient, settings);
            // Act
            var date = await someClient.GetSomeData();
            // Assert
            date.Should().Be("TestDate");
        }
        
        //Edge case
        [Fact]
        public async Task Some_client_returns_null_if_empty_response()
        {
            // Arrange
            var content = new StringContent("{}");
            var settings = new ClientSettings
            {
                ApiUrl = "https://www.google.com",
                TimeZone = "TestZone"
            };
            var handlerMock = new Mock<TestHttpMessageHandler>{CallBase = true};
            using var httpClient = new HttpClient(handlerMock.Object);
            handlerMock
                .Setup(handler => handler.SendTestAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK, 
                    Content = content
                });
            
            var someClient = new SomeClient(httpClient, settings);
            // Act
            var date = await someClient.GetSomeData();
            // Assert
            date.Should().Be(null);
        }
        
        //Edge case
        [Fact]
        public async Task Some_client_throws_if_not_ok()
        {
            // Arrange
            var settings = new ClientSettings
            {
                ApiUrl = "https://www.google.com",
                TimeZone = "TestZone"
            };
            var handlerMock = new Mock<TestHttpMessageHandler>{CallBase = true};
            using var httpClient = new HttpClient(handlerMock.Object);
            var content = new StringContent(string.Empty);
            handlerMock
                .Setup(handler => handler.SendTestAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest, 
                    Content = content
                });
            
            var someClient = new SomeClient(httpClient, settings);
            // Act
            Func<Task> act = async () => await someClient.GetSomeData();
            // Assert
            await act.Should().ThrowAsync<HttpRequestException>();
        }
        
        //Testing logic
        [Fact]
        public async Task Some_client_sends_request_to_valid_url()
        {
            // Arrange
            var settings = new ClientSettings
            {
                ApiUrl = "https://www.google.com/",
                TimeZone = "/TestZone"
            };
            var handlerMock = new Mock<TestHttpMessageHandler>{CallBase = true};
            using var httpClient = new HttpClient(handlerMock.Object);
            var content = new StringContent("{\"Date\":\"TestDate\"}");
            handlerMock
                .Setup(handler => handler.SendTestAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK, 
                    Content = content
                });
            
            var someClient = new SomeClient(httpClient, settings);
            // Act
            await someClient.GetSomeData();
            // Assert
            handlerMock.Verify(handler => handler.SendTestAsync( 
                It.Is<HttpRequestMessage>(message => message.RequestUri.ToString() == "https://www.google.com/timezone/TestZone"),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}